### Other cool stuff #

- DSLs
- Fable
- SAFE stack
- Fabulous

---

### DSLs

F# syntax is human readable and can lead to intuitive internal DSLs

FParsec can be used to create external DSLs

[Talk on creating external 'excel' DSL](https://www.youtube.com/watch?v=Bnm71YEt_lI)

---

### Fable

Inspired by Javascript transpiler Babel. Converts F# code into Javascript:

![FableFs](assets/img/fable.png)

F# can be used as a server and client side language, with code sharing across stack.

---

### SAFE Stack

- S -> Saturn - ASP.NET Core based framework
- A -> Azure/AWS
- F -> Fable
- E -> Elmish(.React) - MVU abstraction over react

---


### MVU Pattern

![MVU](assets/img/mvu.png)

[https://compositional-it.com/blog/2017/09-21-safe-elmish/index.html](https://compositional-it.com/blog/2017/09-21-safe-elmish/index.html)

---

### SAFE stack - Client side 

![FableJs](assets/img/SAFE.png)

[https://safe-stack.github.io/docs/component-elmish/](https://safe-stack.github.io/docs/component-elmish/)

---

### MVU Pattern - Elmish.React

![MVU](assets/img/mvuElmish.png)

---

### Fabulous

A variation of elmish to build Xamarin.Forms applications for iOS, Android, Mac and more. 

The approach to app development is similar to Redux and Flow but targets Xamarin and Xamarin.Forms

---

### MVU Pattern - Fabulous

![MVU](assets/img/mvuFabulous.png)

--- 

### F#ull stack apps

Can write client side apps that share F# domain logic.

Differences between browser and mobile clients are the `view` modules written in `Elmish.React` or `Fabulous`.
