module Client

open Elmish
open Elmish.React
open Fable.Helpers.React
open Fable.PowerPack.Fetch
open Fable.PowerPack.PromiseSeqExtensions
open Fable.Core.JsInterop
open Shared.DataTransfer
open Fulma
open Shared.Domain
open Shared.Validation
open Fable.PowerPack


type Model = { 
    Email: string
    Phone: string 
    SubmittedId: string option 
    Loading: bool
    ErrorMessage: string option
    }


type ApiResponse = 
    | Success of ContactDetailsResult
    | ValidationError of FourTwoTwo
    | Unknown of string

type Msg =
    | EmailChanged of string
    | PhoneChanged of string
    | Submit
    | GotResponse of ApiResponse


let init () : Model * Cmd<Msg> =
    let initialModel = { 
        Email = ""
        Phone = ""
        SubmittedId = None
        Loading = false
        ErrorMessage = None }

    initialModel, Cmd.none

let tryPost<'T> (url: string) (record:'T) (properties: RequestProperties list) =
    let defaultProps =
        [ RequestProperties.Method HttpMethod.POST
        ; requestHeaders [ContentType "application/json"]
        ; RequestProperties.Body !^(toJson record)]
    let init = List.append defaultProps properties
    GlobalFetch.fetch(RequestInfo.Url url, requestProps init)
    |> Fable.PowerPack.Promise.map (fun response ->
        if response.Ok then 
            Ok response
        else 
            Error response
    )

let postContact model =
    let promise m = 
        tryPost "/api/contact" { email = Email m.Email; phone = Phone m.Phone } []
        |> Fable.PowerPack.Promise.bind (fun result -> 
            match result with
            | Ok response -> 
                response.json<ContactDetailsResult>()
                    .``then``(Success)
                    .``catch``(fun x -> Unknown (x.ToString()))
            | Error response ->
                match response.Status with
                | 422 -> response.json<FourTwoTwo>()
                            .``then``(ValidationError)
                            .``catch``(fun x -> Unknown (x.ToString()))
                | _ -> Fable.Import.JS.Promise.resolve(Unknown response.StatusText)
        )
        |> Fable.PowerPack.Promise.map GotResponse
                        
    Cmd.ofPromise promise model id (fun x -> GotResponse(Unknown (x.Message)))
        

let update (msg : Msg) (currentModel : Model) : Model * Cmd<Msg> =
    let printErrors errors = 
        let mutable error = ""
        for e in errors do
            let sep = if error = "" then "" else ", " 
            error <- error + sep + e.Error
        Some error

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
            { currentModel with Loading = false; ErrorMessage = printErrors four22.errors }, Cmd.none
        
        | Unknown error -> 
            { currentModel with Loading = false; ErrorMessage = Some error }, Cmd.none
    
let view (model : Model) (dispatch : Msg -> unit) =

    let errorOrBlankDiv (model : Model) =
        match model.ErrorMessage with
        | Some error -> Heading.h4 [] [str(error)]
        | None -> div [] []

    let loadingOrBlankDiv (model : Model) =
        if model.Loading then
            Heading.h4 [] [str("Loading...")]
        else 
            div [] []

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
