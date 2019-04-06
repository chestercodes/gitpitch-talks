namespace Shared
open System.Text.RegularExpressions

module DataTransfer =
    type ContactDetails = { email: string; phone: string }
    type ContactDetailsResult = { Id: string }

module Validation =
    
    type ValidationError = { Tag: string; Error: string }
    
    type ValidationResults = 
        | Passed
        | Failed of ValidationError list
    
    let validateEmail (contactDetails: DataTransfer.ContactDetails) =
        let regex = Regex("^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-.]+$")
        if regex.IsMatch contactDetails.email then
            None
        else
            Some { Tag = "email"; Error = "Email doesnt match regex" }        
    
    let validatePhone (contactDetails: DataTransfer.ContactDetails) =
        let regex = Regex("^[0-9\\s]+$")
        if regex.IsMatch contactDetails.phone then
            None
        else
            Some { Tag = "phone"; Error = "Phone doesnt match regex" }        
    
    let validateContactDetails (contactDetails: DataTransfer.ContactDetails) =
        let contactDetailsValidations = [ validateEmail; validatePhone ]

        let validationErrors = 
            contactDetailsValidations 
            |> List.map (fun validator -> validator contactDetails)
            |> List.choose id

        match validationErrors with
        | [] -> Passed
        | invalid -> Failed invalid

module Responses =
    type FourTwoTwo = { errors: Validation.ValidationError list }
