module Client

open Elmish
open Elmish.React
open Fable.Helpers.React
open Fulma

type Counter = { Value : int }

type Model = { Counter: Counter }

type Msg =
    | Increment
    | Decrement

let update (msg : Msg) (model : Model) : Model * Cmd<Msg> =
    match msg with
    | Increment ->
        let nextModel = { model with Counter = { Value = model.Counter.Value + 1 } }
        nextModel, Cmd.none
    | Decrement ->
        let nextModel = { model with Counter = { Value = model.Counter.Value - 1 } }
        nextModel, Cmd.none

let view (model : Model) (dispatch : Msg -> unit) =
    let counterMessage = "Press buttons to manipulate counter: " + string model.Counter.Value
    
    let button txt onClick =
        Button.button [ Button.IsFullWidth; Button.Color IsPrimary; Button.OnClick onClick ]
            [ str txt ]

    div [] [ 
        Navbar.navbar [ Navbar.Color IsPrimary ]
            [ Navbar.Item.div [ ] [ Heading.h2 [ ] [ str "SAFE Template" ] ] ]

        Container.container [] [ 
                Content.content [ Content.Modifiers [ Modifier.TextAlignment (Screen.All, TextAlignment.Centered) ] ]
                    [ Heading.h3 [] [ str counterMessage ] ]
                Columns.columns [] [ 
                    Column.column [] [ button "-" (fun _ -> dispatch Decrement) ]
                    Column.column [] [ button "+" (fun _ -> dispatch Increment) ] ] ] 
        ]

let init () : Model * Cmd<Msg> =
    let initialModel = { Counter = { Value = 42 } }
    initialModel, Cmd.none

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
