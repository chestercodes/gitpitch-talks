namespace fs.Shared

type Address = { Line1: string; Line2: string; PostCode: string }

type Phone = Phone of string
type Email = Email of string
type Contact = 
    | JustEmail of Email
    | JustPhone of Phone
    | EmailAndPhone of Email * Phone
    | SomethingElse

module Utils =
    let emailValue (Email email) = email
    let phoneValue (Phone phone) = phone

    let contactInfoToString contact =
        match contact with
        | JustEmail email -> sprintf "Email is %s" (emailValue email)
        | JustPhone phone -> sprintf "Phone is %s" (phoneValue phone)
        | EmailAndPhone (email, phone) -> 
            sprintf "Email is %s, Phone is %s" (emailValue email) (phoneValue phone)


