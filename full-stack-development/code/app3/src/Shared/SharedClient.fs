namespace SharedClient
open Shared
open Shared.DataTransfer
open Shared.Responses
open Shared.Validation

#if MOBILE
open Fabulous.Core
#else
open Elmish
#endif


module ModelUpdate =

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

    let updatePartial (postContact: Model -> Cmd<Msg>) =
        fun (msg : Msg) (currentModel : Model) -> // : Model * Cmd<Msg>
            let printErrors errors = 
                let mutable error = ""
                for e in errors do
                    let sep = if error = "" then "" else ", " 
                    error <- error + sep + e.Error
                Some error
            
            let validateNewEmailOrPhone email phone = 
                match validateContactDetails {email = email; phone = phone} with
                | Passed -> None
                | Failed errors -> printErrors errors
            
            match msg with
            | EmailChanged newEmail -> 
                { currentModel with Email = newEmail; ErrorMessage = (validateNewEmailOrPhone newEmail currentModel.Phone) }, Cmd.none
            
            | PhoneChanged newPhone -> 
                { currentModel with Phone = newPhone; ErrorMessage = (validateNewEmailOrPhone currentModel.Email newPhone) }, Cmd.none
            
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
