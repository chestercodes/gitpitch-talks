open System
open System.Text.RegularExpressions


module Domain =
    
    type UnvalEmail = Email of string
    
    type UnvalPhone = Phone of string
    
    type ValidationError = { Tag: string; Error: string }
    
    type ValidationResults = 
        | Passed
        | Failed of ValidationError list
    

module Validation =
    open Domain

    let emailValidationError message =
        Some { Tag = "email"; Error = message }

    let phoneValidationError message =
        Some { Tag = "phone"; Error = message }

    let emailCantBeBlank (Email email, Phone _) =
        if String.IsNullOrWhiteSpace email then
            emailValidationError "Email is blank"
        else        
            None    
    
    let emailDoesNotMatchRegex (Email email, Phone _) =
        let regex = Regex("^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-.]+$")
        if regex.IsMatch email then
            None
        else
            emailValidationError "Email doesnt match regex"
    
    let phoneCantBeBlank (Email _, Phone phone) =
        if String.IsNullOrWhiteSpace phone then
            phoneValidationError "Phone is blank"
        else        
            None

    let phoneDoesNotMatchRegex (Email _, Phone phone) =
        let regex = Regex("^[0-9\\s]+$")
        if regex.IsMatch phone then 
            None
        else 
            phoneValidationError "Phone doesnt match regex"
    
    let validateContactDetails unvalEmail unvalPhone =
        let validators = [   
                emailCantBeBlank
                phoneCantBeBlank 
                emailDoesNotMatchRegex
                phoneDoesNotMatchRegex 
            ]

        let validationErrors = 
            validators
            |> List.map (fun validator -> 
                validator (unvalEmail, unvalPhone)
            )
            |> List.filter (fun validationError -> validationError.IsSome)
            |> List.map (fun validationError -> validationError.Value)

        match validationErrors with
        | [] -> Passed
        | errors -> Failed errors
