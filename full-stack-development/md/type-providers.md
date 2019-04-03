
## Type providers

---

@quote[The world is information rich.]
@quote[Our languages are information sparse]
@quote[We need to bring information into the language... At internet-scale, strongly tooled, strongly typed](https://www.slideshare.net/dsyme/making-magic-with-f-type-providers)


---

@quote[An F# type provider is a component that provides types, properties, and methods for use in your program]

---

### Type Provider Example

Let's make our first F# program!

---?code=code/ps/CreateDemoProject.ps1&lang=ps

@[1-8](Create a new dotnet console project with -lang parameter F#)
@[14-17](Install type providers from nuget packages)

---

### Existing type providers

Json, Xml, Csv, Html

Sql, Azure Storage, GraphQL

R ...

---

### RProvider

Can send calculation about bank accounts to be plotted by R

---?code=code/all/Demo/Program.fs&lang=fs

@[26-29](Install RProvider and open in code)
@[105-115](Calculate amounts for banks)
@[117-124](Calculate graph limits)
@[131-138](Create blank graph with R.plot)
@[140-148](Add R.lines for each bank)
@[153-161](Add legends and title)

---

![BanksGraph](assets/img/banks.png) 

