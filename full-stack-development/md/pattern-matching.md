### Pattern matching

@quote[Never put off until run time what can be done at compile time.]

---

Pattern matching is ubiquitous in F#. 

It is used for:

---?code=code/match/intro.fsx&lang=fsharp

@[1-4]()
@[4-9]()
@[9-14]()

---

### Scenario

Fizzbuzz - Players take turns to count incrementally, replacing

- any number divisible by three with the word "Fizz"
- any number divisible by five with the word "Buzz"
- numbers divisible by both become "Fizz Buzz".


---?code=code/match/fizz.cs&lang=csharp

---?code=code/match/fizz.fsx&lang=fsharp

@[1](Define function fizzbuzz that takes arg n. int type inferred from body)
@[2-6](match expression for function body)
@[2,8](matches input n with)
@[3,9](n that is divisible by 15 returns "FizzBuzz")
@[4-5](similar for 3 and 5)
@[6](everything else is caught by wildcard match and returns ToString())

---?code=code/match/active.fsx&lang=fsharp

@[1-13](Can use F# Active Patterns to seperate classification from outcome)
@[1-6](Active pattern shows classifies input into specified states)
@[8-13](Matching on active pattern gives outcome )

---

Real world example.