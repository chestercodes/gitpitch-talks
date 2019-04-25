namespace SharedClient
open Shared.DataTransfer
open Shared.Domain
open Shared.Validation

#if MOBILE
open Fabulous.Core
#else
open Elmish
#endif


module ModelUpdate =

    type SubmitStatus =
        | ToBeSubmitted
        | AwaitingResponse
        | SubmittedOk of string
        | Error of string
        
    type Model = { 
        Email: string
        Phone: string 
        Status: SubmitStatus
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
            Status = ToBeSubmitted }

        initialModel, Cmd.none

    let updatePartial (postContact: Model -> Cmd<Msg>) =

        fun (msg : Msg) (currentModel : Model) ->
        
            let getStatus email phone =
                match validateContactDetails (Email email) (Phone phone) with
                | Passed -> ToBeSubmitted
                | Failed errors -> Error(printErrors errors)

            match msg with
            | EmailChanged newEmail -> 
                { currentModel with Email = newEmail; Status = getStatus newEmail currentModel.Phone }, Cmd.none
            
            | PhoneChanged newPhone -> 
                { currentModel with Phone = newPhone; Status = getStatus currentModel.Email newPhone }, Cmd.none
            
            | Submit ->
                let cmd = postContact currentModel
                { currentModel with Status = AwaitingResponse }, cmd
            
            | GotResponse response ->
                match response with
                | Success resp -> 
                    { currentModel with Status = SubmittedOk resp.Id }, Cmd.none
                
                | ValidationError four22 ->
                    { currentModel with Status = Error(printErrors four22.errors) }, Cmd.none
                
                | Unknown error -> 
                    { currentModel with Status = Error error }, Cmd.none
