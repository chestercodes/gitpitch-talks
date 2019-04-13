
## SAFE stack 
## ->
## EFAS kcats

---

### E - Elmish (Elm-ish)
  
MVU pattern abstractions library. Main components and type signatures:

- Model: `Model` - Application's state, immutable

- Message: `Msg` - change in application state

- Command: `Cmd<Msg>` - When evaluated may produce message(s)

---

- Init: `unit -> Model * Cmd<Msg>`
- - produces the inital applications state and commands

- Update: `Msg -> Model -> Model * Cmd<Msg>`
- - produces new state of application given the previous and message

- View: `Model -> (Msg -> unit) -> 'ui `
- - produces a new UI layout/content given the current state.

---

### Elmish types

![MVU](full-stack-development/assets/img/mvuTypes.png)

---

## F - Fable

## ( F# Babel )

---

## Babel

- JavaScript evolves faster than browser usage
- Babel is a JavaScript transpiler
- JavaScript/Other -> legacy browser compatible JS

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

- Typed access to native JavaScript apis

