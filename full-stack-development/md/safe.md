
## SAFE stack 
## ->
## EFAS kcats

---

### E - Elmish (Elm-ish)
  
MVU pattern library.

- Model - Application's state, immutable

- Message - A change in application state

- Command - When evaluated may produce message(s)

---

- Init - produces inital state and commands

- Update - produces new state of application given previous and message

- View - produces UI layout/content given the current state.

---

### Elmish with type signatures

![MVU](full-stack-development/assets/img/mvuTypes.png)

---

## F - Fable

## ( F# Babel )

---

## Babel

- JavaScript evolves faster than browser usage
- transpiles modern JavaScript to legacy browser compatible JS

```javascript
// ES6
[1, 2, 3].map((n) => n + 1)
```

```javascript
// ES5 and below
[1, 2, 3].map(function(n) {
  return n + 1
})
```

---

## Fable

- Transpiler from F# -> JavaScript

- F# code AST to JS, language level, not IL

- Implementation of some .NET API equivalents

- Typed access to native JavaScript apis

---

F#

```fsharp
let addOne arg1 = 
    arg1 + 1
```

JavaScript

```javascript
export function addOne(arg1) {
  return arg1 + 1;
}
```

---

F#

```fsharp
let aRandomInt = (new System.Random()).Next(5)
```

JavaScript

```javascript
import { randomNext } from "fable-library/Util.js";
export const aRandomInt = randomNext(0, 5);
```

---

![FableElmish](full-stack-development/assets/img/mvuFable.png)

---

### A - Azure / AWS 

How applications tend to be hosted.

---

### S - Saturn

- Server-side functional MVC web development framework.

- Top-level abstractions over ASP.NET Core, similar concepts. 

- Includes scaffolding tools for rapid application development

---

### Safe full stack

![FullStack](full-stack-development/assets/img/safeFullStack.png)

https://safe-stack.github.io/docs/overview/

---

### SAFE app

gather user contact information. Want to have:

- Inputs for email and phone.
- Submit button posts to server
- Validation on server and return contact guid if valid.
- If not valid then display errors.


---?code=full-stack-development/code/scripts/CreateSafeApp.ps1&lang=ps

---

![Files](full-stack-development/assets/img/SafeNewFilesAnnotated.jpg)

---

### Code demo

---

### Code sharing

| Module       | Server | Browser |
| ------------ | ------ | ------  |
| Domain       |  yes   |   yes   |
| DataTransfer |  yes   |   yes   |
| Validation   |  yes   |   yes   |
