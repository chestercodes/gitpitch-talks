
## SAFE stack 
## ->
## EFAS kcats

---

### E - Elmish (Elm-ish)
  
MVU pattern abstractions library.

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

- Implementation of some .NET API equivalents

- Typed access to native JavaScript apis
