// Copyright 2018 Fabulous contributors. See LICENSE.md for license.
namespace Mobile

open SharedClient.ModelUpdate
open Fabulous.Core
open Fabulous.DynamicViews
open Xamarin.Forms
open System

module App = 
    
    let postContact (model:Model) =
        async {
            do! Async.Sleep 200
            let msg =
                if model.ErrorMessage.IsSome then
                    GotResponse(Unknown "Validations are incorrect")
                else
                    GotResponse(Success { Id = Guid.NewGuid().ToString()})
            return msg }
        |> Cmd.ofAsyncMsg

    let view (model: Model) dispatch =
        View.ContentPage(
          content = View.StackLayout(padding = 20.0, verticalOptions = LayoutOptions.Center,
            children =
                if model.SubmittedId.IsNone then
                    [
                        View.Label(text = "Email")
                        View.Entry(text = "", textChanged = (fun args -> dispatch (EmailChanged(args.NewTextValue))))

                        View.Label(text = "Phone")
                        View.Entry(text = "",textChanged = (fun args -> dispatch (PhoneChanged(args.NewTextValue))))

                        View.Button(text = "Submit", command = (fun () -> dispatch Submit), horizontalOptions = LayoutOptions.Center)

                        View.Label(text=(if model.ErrorMessage.IsSome then model.ErrorMessage.Value else ""), textColor = Xamarin.Forms.Color.Red)
                    ]
                else [
                    View.Label(text = if model.SubmittedId.IsSome then sprintf "Thank you: %s" model.SubmittedId.Value else "")
                ]
            )
        )

    // Note, this declaration is needed if you enable LiveUpdate
    let program = Program.mkProgram init (updatePartial postContact) view

type App () as app = 
    inherit Application ()

    let runner = 
        App.program
#if DEBUG
        |> Program.withConsoleTrace
#endif
        |> Program.runWithDynamicView app

#if DEBUG
    // Uncomment this line to enable live update in debug mode. 
    // See https://fsprojects.github.io/Fabulous/tools.html for further  instructions.
    //
    //do runner.EnableLiveUpdate()
#endif    

    // Uncomment this code to save the application state to app.Properties using Newtonsoft.Json
    // See https://fsprojects.github.io/Fabulous/models.html for further  instructions.
#if APPSAVE
    let modelId = "model"
    override __.OnSleep() = 

        let json = Newtonsoft.Json.JsonConvert.SerializeObject(runner.CurrentModel)
        Console.WriteLine("OnSleep: saving model into app.Properties, json = {0}", json)

        app.Properties.[modelId] <- json

    override __.OnResume() = 
        Console.WriteLine "OnResume: checking for model in app.Properties"
        try 
            match app.Properties.TryGetValue modelId with
            | true, (:? string as json) -> 

                Console.WriteLine("OnResume: restoring model from app.Properties, json = {0}", json)
                let model = Newtonsoft.Json.JsonConvert.DeserializeObject<App.Model>(json)

                Console.WriteLine("OnResume: restoring model from app.Properties, model = {0}", (sprintf "%0A" model))
                runner.SetCurrentModel (model, Cmd.none)

            | _ -> ()
        with ex -> 
            App.program.onError("Error while restoring model found in app.Properties", ex)

    override this.OnStart() = 
        Console.WriteLine "OnStart: using same logic as OnResume()"
        this.OnResume()
#endif


