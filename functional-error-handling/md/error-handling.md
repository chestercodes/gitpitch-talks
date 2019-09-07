### Exceptions

---?code=functional-error-handling/code/ExceptionCode.cs&lang=cs

@[1-6](Create array and retrieve value)
@[7-14](Jumps to where?)
@[7,11-14](Trick question, throws an IndexOutOfRangeException)

---

### so?

- Exceptions are gotos, but worse, invisible gotos.
- Exceptions create many invisible exit points.

Another way?

---

Wrap dangerous operation in data structure, called `Result` type.

`Result` can be one of 2 types `TOk` if the operation was ok and `TError` if there was an error to handle.



