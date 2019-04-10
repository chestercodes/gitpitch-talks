
### Fable and Elmish

![Elmish](full-stack-development/assets/img/elmishLayers.png)

https://safe-stack.github.io/docs/component-elmish/

---

### A - Azure / AWS 

How applications tend to be hosted.

---

### S - Saturn (/ Suave)

- Server-side MVC web development framework written in F#, heavily inspired by Elixir's Phoenix.

- Commonly used with ASP.NET Core, served by Kestrel. Can be used with Giraffe.

- Includes scaffolding tools for rapid application development

---

### Saturn - concepts

- Application - start/end of request, configuration, hands to router

- Router - define pipelines to pass requests, parses requests and dispatches to controller

- Controller - contains actions to handle requests, redirect, return views or data

- Views - render templates

---

### Safe full stack

![FullStack](full-stack-development/assets/img/safeFullStack.png)

https://safe-stack.github.io/docs/overview/

---

### app

app for user contact information. Want to have:

- Inputs for email and phone.
- Submit button posts to server
- Validation on server and return contact guid if valid.
- If not valid then display errors.

Lets make an app!

---?code=full-stack-development/code/scripts/CreateSafeApp.ps1&lang=ps


