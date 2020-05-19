## make it the compilers fault ##

---

## The bug pain ladder

Bugs can be caught/squashed at many stages.

- Production (runtime)
- Staging (fake runtime)
- Broader tests (application runtime)
- Unit Tests (module runtime)
- Compiler
- While in head

Tech(nologies/niques) that move bug discoveries down the ladder are good.

---

@snap[north-east span-20]
@quote[I love types](Me)
@snapend

---

@snap[north-east span-20]
@quote[A good type-system is worth a thousand unit tests](Henry VII)
@snapend

--- 

Types have advantages that they:

- Move errors down the ladder to the compiler stage
- constrain execution paths
- act as system documentation

--- 

Disadvantages:

- take longer to write code
- can be annoying and verbose
- weaker type systems allow/encourage questionable code reuse techniques (inheritance)

--- 

## Demo

Have a web service that returns a `User` object given a tenant and user ids in the form of guids

---

