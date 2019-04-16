module Client

open Elmish
open Elmish.React
open Fable.Helpers.React
open Fable.PowerPack.Fetch
open Fable.Core.JsInterop
open Shared.DataTransfer
open SharedClient.ModelUpdate
open Shared.Responses
open Fulma


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
        tryPost "/api/contact" { email = m.Email; phone = m.Phone } []
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

Program.mkProgram init (updatePartial postContact) view
#if DEBUG
|> Program.withConsoleTrace
|> Program.withHMR
#endif
|> Program.withReact "elmish-app"
#if DEBUG
|> Program.withDebugger
#endif
|> Program.run
