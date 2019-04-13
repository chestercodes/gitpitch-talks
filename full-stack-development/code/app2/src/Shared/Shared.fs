namespace Shared
open System
open System.Text.RegularExpressions

module DataTransfer =
    
    type ContactDetails = { email: string; phone: string }
    
    type ContactDetailsResult = { Id: string }


module Validation =
    
    type ValidationError = { Tag: string; Error: string }
    
    type ValidationResults = 
        | Passed
        | Failed of ValidationError list
    
    let emailValidationError message =
        Some { Tag = "email"; Error = message }

    let emailCantBeBlank (contactDetails: DataTransfer.ContactDetails) =
        if String.IsNullOrWhiteSpace contactDetails.email then
            emailValidationError "Email is blank"
        else        
            None    
    
    let emailDoesNotMatchRegex (contactDetails: DataTransfer.ContactDetails) =
        let regex = Regex("^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-.]+$")
        if regex.IsMatch contactDetails.email then
            None
        else
            emailValidationError "Email doesnt match regex"
    
    let phoneValidationError message =
        Some { Tag = "phone"; Error = message }

    let phoneCantBeBlank (contactDetails: DataTransfer.ContactDetails) =
        if String.IsNullOrWhiteSpace contactDetails.phone then
            phoneValidationError "Phone is blank"
        else        
            None

    let phoneDoesNotMatchRegex (contactDetails: DataTransfer.ContactDetails) =
        let regex = Regex("^[0-9\\s]+$")
        if regex.IsMatch contactDetails.phone then 
            None
        else 
            phoneValidationError "Phone doesnt match regex"
    
    let validateContactDetails (contactDetails: DataTransfer.ContactDetails) =
        let validationErrors = 
            [   emailCantBeBlank
                phoneCantBeBlank 
                emailDoesNotMatchRegex
                phoneDoesNotMatchRegex ]
            |> List.map (fun validator -> validator contactDetails)
            |> List.choose id

        match validationErrors with
        | [] -> Passed
        | errors -> Failed errors


module Responses =

    type FourTwoTwo = { errors: Validation.ValidationError list }

                                                                                                                            //