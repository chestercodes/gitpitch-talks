## SAFE stack

---

### S - Saturn / Suave


---

### A - Azure / AWS 

---

### F - Fabel

---

### E - Elmish

Abstractions that can be used to build applications following the “model view update” style of architecture, as made famous by Elm.

- Model - Application's state, immutable.
- Message - change in application state
- Command - When evaluated may produce message(s).

---

- Init - This is a pure function that produces the inital state of your application and, optionally, commands to process.
- Update - This is a pure function that produces a new state of your application given the previous state and, optionally, new commands to process.
- View - This is a pure function that produces a new UI layout/content given the current state.
