
### SAFE app

gather user contact information. Want to have:

- Inputs for email and phone.
- Submit button posts to server
- Validation on server and return contact guid if valid.
- If not valid then display errors.

Lets make an app!

---?code=full-stack-development/code/scripts/CreateSafeApp.ps1&lang=ps

---

![Files](full-stack-development/assets/img/SafeNewFilesClient.jpg)

---

### app - Shared.fs

---?code=full-stack-development/code/app2/src/Shared/Shared.fs&lang=fs
@[1-9](Define Shared namespace and DataTransfer module. Can be accessed at Shared.DataTransfer)
@[12-14](Validation module contains Record for storing error)
@[16-18](ValidationResults is a discriminated union. Can either be Passed or Failed which is a list of ValidationError)
@[20-27](validation functions to return an option of ValidationError )
@[52-59](validation function takes ContactDetails, applies all of validation functions)
@[61-63](pattern matching returns Passed if there are no Some<ValidationError> and Failed of the errors if they exist.)
@[66-70](Define validation response for validation errors returned from server)

--- 

### Code sharing

F# modules can be imported into dotnet projects. 

3 defined modules will used in server side code:

| Module       | Server |
| ------------ | ------ |
| DataTransfer |  yes   |
| Validation   |  yes   |
| Responses    |  yes   |

