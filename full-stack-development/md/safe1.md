
## SAFE stack 
## ->
## EFAS kcats

---

### E - Elmish

Abstractions that can be used to build applications following the “model view update” style of architecture, as made famous by Elm.

- Model `: Model` - Application's state, immutable
- Message `: Msg` - change in application state
- Command `: Cmd<Msg>` - When evaluated may produce message(s)

---

- Init `: unit -> Model * Cmd<Msg>`
- - produces the inital applications state and commands
- Update `: Msg -> Model -> Model * Cmd<Msg>`
- - produces new state of application given the previous and message
- View `: Model -> (Msg -> unit) -> 'ui `
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
[1, 2, 3].map((n) => n + 1)
```

```javascript
[1, 2, 3].map(function(n) {
  return n + 1
})
```

---

## Fable

- Transpiler from F# -> JavaScript
- Typed access to native JavaScript apis
- Converts F# code AST to JS, language level, not IL

