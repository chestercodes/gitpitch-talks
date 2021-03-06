### Exceptions

---?code=functional-error-handling/code/ExceptionCode.cs&lang=cs

@[1-6](Create array and retrieve value)
@[7-14](Jumps to where?)
@[7,11-14](Trick question, throws an IndexOutOfRangeException)

---

### so?

- Exceptions are gotos, but worse, invisible gotos.
- Exceptions create many invisible exit points.
- Errors not explicit in type system

Another way?

---

Wrap dangerous operation in data structure, called `Result` type.

`Result` wraps a value of one of 2 types:

- `TOk` if the operation was ok
- `TError` if there was an error

---

Programs become a pipeline of `Result<TOk,TError>` types.

- HttpRequest -> 
- `Result<TOk1,TError>` ->
- `Result<TOk2,TError>` -> 
- ... -> 
- HttpResponse

---

First operation creates `Result` 

![Pipeline1](functional-error-handling/assets/img/Pipeline1.png)

---

Second operation takes `Result` and applies function if `TOk`, does nothing if `TError`

![Pipeline2](functional-error-handling/assets/img/Pipeline2.png)

---

And so on until end function takes `Result` and returns Response

![Pipeline3](functional-error-handling/assets/img/Pipeline3.png)

Similar behaviour to:

```cs
var subSubPropOrNull = data?.Prop?.SubProp?.SubSubProp
```

---

### Function names

![Terms](functional-error-handling/assets/img/Terms.png)

---

### Result in C# ###


---?code=functional-error-handling/code/Downloader.Api/Other/Result.cs&lang=cs

@[3-7](Generic abstract class that can only be created as subclasses)
@[21-29](Ok subclass)
@[31-39](Error subclass)
@[9-19](Functions to create and query result)


---?code=functional-error-handling/code/Downloader.Api/Other/Extensions.cs&lang=cs

@[7-14](Extension method for bind)
@[23-30](Extension method for map)
@[33-39](Match applies success/fail functions to Result to retrieve value)
