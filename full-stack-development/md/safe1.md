
## SAFE stack 
## ->
## EFAS kcats

---

### E - Elmish

Abstractions that can be used to build applications following the “model view update” style of architecture, as made famous by Elm.

- Model - Application's state, immutable
- Message - change in application state
- Command - When evaluated may produce message(s)

---

- Init - produces the inital applications state and commands
- Update - produces new state of application given the previous and message
- View - produces a new UI layout/content given the current state.

---

### F - Fabel

### ( F# Babel )

---

### Babel

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

### Fabel

- Transpiler from F# -> JavaScript
- Typed access to native JavaScript apis