## SAFE stack

---

### S - Saturn / Suave


---

### A - Azure / AWS 

---

### F - Fabel

---

### E - Elmish

Elmish implements core abstractions that can be used to build Fable applications following the “model view update” style of architecture, as made famous by Elm.

- Model - Application's state, defined as an immutable data structure.
- Message - Event representing a change in the state of application
- Command - Carrier of instructions, that when evaluated may produce one or more messages.
- Init - This is a pure function that produces the inital state of your application and, optionally, commands to process.
- Update - This is a pure function that produces a new state of your application given the previous state and, optionally, new commands to process.
- View - This is a pure function that produces a new UI layout/content given the current state.
