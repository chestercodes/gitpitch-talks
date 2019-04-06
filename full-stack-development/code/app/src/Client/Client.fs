module Client

open Elmish
open Elmish.React

open Fable.Helpers.React

open Fable.PowerPack.Fetch
open Fable.PowerPack.PromiseSeqExtensions
open Fable.Core.JsInterop
open Shared.DataTransfer



open Fulma
open Shared.Responses
open Shared.Validation
open Fable.PowerPack


// The model holds data that you want to keep track of while the application is running
// in this case, we are keeping track of a counter
// we mark it as optional, because initially it will not be available from the client
// the initial value will be requested from server
type Model = { 
    Email: string
    Phone: string 
    SubmittedId: string option 
    Loading: bool
    ErrorMessage: string option
    }

// The Msg type defines what events/actions can occur while the application is running
// the state of the application changes *only* in reaction to these events
type ApiResponse = 
    | Success of ContactDetailsResult
    | ValidationError of FourTwoTwo
    | Unknown of string

type Msg =
    | EmailChanged of string
    | PhoneChanged of string
    | Submit
    | GotResponse of ApiResponse

// defines the initial state and initial command (= side-effect) of the application
let init () : Model * Cmd<Msg> =
    let initialModel = { 
        Email = ""
        Phone = ""
        SubmittedId = None
        Loading = false
        ErrorMessage = None }

    initialModel, Cmd.none

let tryPostRecord2<'T> (url: string) (record:'T) (properties: RequestProperties list) =
    let defaultProps =
        [ RequestProperties.Method HttpMethod.POST
        ; requestHeaders [ContentType "application/json"]
        ; RequestProperties.Body !^(toJson record)]
    // Append properties after defaultProps to make sure user-defined values
    // override the default ones if necessary
    let init = List.append defaultProps properties
    GlobalFetch.fetch(RequestInfo.Url url, requestProps init)
    |> Fable.PowerPack.Promise.map (fun response ->
        if response.Ok
        then Ok response
        else Error response)

let postContact model =
    let promise m = 
        tryPostRecord2 "/api/contact" { email = m.Email; phone = m.Phone } []
        |> Fable.PowerPack.Promise.bind (fun result -> 
            match result with
            | Ok response -> response.json<ContactDetailsResult>().``then``(Success)
            | Error response ->
                match response.Status with
                | 422 -> response.json<FourTwoTwo>()
                            .``then``(ValidationError)
                            .``catch``(fun x -> Unknown (x.ToString()))
                | _ -> Fable.Import.JS.Promise.resolve(Unknown response.StatusText)
        )
        |> Fable.PowerPack.Promise.map GotResponse
                        
    Cmd.ofPromise promise model id (fun x -> GotResponse(Unknown (x.Message)))
        
// The update function computes the next state of the application based on the current state and the incoming events/messages
// It can also run side-effects (encoded as commands) like calling the server via Http.
// these commands in turn, can dispatch messages to which the update function will react.
let update (msg : Msg) (currentModel : Model) : Model * Cmd<Msg> =
    match msg with
    | EmailChanged newEmail -> 
        { currentModel with Email = newEmail }, Cmd.none
    
    | PhoneChanged newPhone -> 
        { currentModel with Phone = newPhone }, Cmd.none
    
    | Submit ->
        let cmd = postContact currentModel
        { currentModel with Loading = true; ErrorMessage = None }, cmd
    
    | GotResponse response ->
        match response with
        | Success resp -> 
            { currentModel with Loading = false; ErrorMessage = None; SubmittedId = Some resp.Id }, Cmd.none
        
        | ValidationError four22 ->
            let mutable message = ""
            for error in four22.errors do
                let start = if message <> "" then ", " else ""
                message <- message + start + error.Error

            { currentModel with Loading = false; ErrorMessage = Some message }, Cmd.none
        
        | Unknown error -> 
            { currentModel with Loading = false; ErrorMessage = Some error }, Cmd.none
    
let errorOrBlankDiv (model : Model) =
    match model.ErrorMessage with
    | Some error -> Heading.h4 [] [str(error)]
    | None -> div [] []

let loadingOrBlankDiv (model : Model) =
    match model.Loading with
    | true -> Heading.h4 [] [str("Loading...")]
    | false -> div [] []

let view (model : Model) (dispatch : Msg -> unit) =
    div []
        [ 
            Navbar.navbar [ Navbar.Color IsPrimary ]
                [ Navbar.Item.div [ ]
                    [ Heading.h2 [ ]
                        [ str "Deets" ] ] ]

            Container.container []
                [ 
                    Content.content [ Content.Modifiers [ Modifier.TextAlignment (Screen.All, TextAlignment.Centered) ] ]
                        [ Heading.h3 [] [ str ("Post your details: ") ] ]
                    (
                        match model.SubmittedId with
                        | Some id -> Text.span [] [ str( "Thank you: " + id ) ]
                        | None -> 
                            div [] [
                                Columns.columns [] [ 
                                        Column.column [] [ Text.span [] [ str( "Email:" ) ] ]
                                        Column.column [] [ Input.text [ Input.OnChange (fun x -> dispatch (EmailChanged x.Value)) ] ]
                                    ] 
                                
                                Columns.columns [] [ 
                                        Column.column [] [ Text.span [] [ str( "Phone:" ) ] ]
                                        Column.column [] [ Input.text [ Input.OnChange (fun x -> dispatch (PhoneChanged x.Value)) ] ]
                                    ] 
                                
                                Button.button [ Button.IsFullWidth; Button.Color IsPrimary; Button.OnClick (fun _ -> dispatch Submit) ]
                                    [ str "Submit" ]

                                loadingOrBlankDiv model
                                errorOrBlankDiv model
                            ]
                    )
                ]
        ]

#if DEBUG
open Elmish.Debug
open Elmish.HMR
#endif

Program.mkProgram init update view
#if DEBUG
|> Program.withConsoleTrace
|> Program.withHMR
#endif
|> Program.withReact "elmish-app"
#if DEBUG
|> Program.withDebugger
#endif
|> Program.run
