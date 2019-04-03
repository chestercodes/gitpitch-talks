type Email = Email of string
let createEmail email = 
    // plus validation
    Email email

type Phone = Phone of string
let createPhone phone = 
    // plus validation
    Phone phone

type Contact = 
    | JustEmail of Email
    | JustPhone of Phone
    | EmailAndPhone of Email * Phone

let email = createEmail "some@email.com"
let phone = createPhone "01234 567890"

let emailOnlyContact = JustEmail email
let phoneOnlyContact = JustPhone phone
let emailAndPhoneContact = EmailAndPhone (email, phone)

let emailValue (Email email) = email
let phoneValue (Phone phone) = phone

let printOutContactInfo contact =
    match contact with
    | JustEmail email -> 
        printfn "Email is %s" (emailValue email)
    | JustPhone phone -> 
        printfn "Phone is %s" (phoneValue phone)
    | EmailAndPhone (email, phone) -> 
        printfn "Email is %s, Phone is %s" 
            (emailValue email) 
            (phoneValue phone)