### F# 101 - Type signatures

functional programmers converse in type signatures.

---?code=full-stack-development/code/typesig.fsx&lang=fs

@[1-2](Tuple type signature)
@[4-6](simple function signature)
@[8-11](simple function signature)
@[4-6,13-14](with C# equivalent)
@[8-11,16-18](with C# equivalent)

---


Native JavaScript

```fsharp

let mutable private arr = [| 1; 2; 3; 4; 5 |]

let str = Fable.Import.JS.JSON.stringify arr 

Fable.Import.JS.console.log(str)
                                                                                                   //
```

```javascript

let arr = new Int32Array([1, 2, 3, 4, 5]);

export const str = JSON.stringify(arr);

console.log(str);
                                                                                                   //
```

---

Not always pretty output

```fsharp

type Person = { 
    Age: int
    Name: string }

type Contact =
    | Email of string
    | Phone of string
                                                                                              //
```

---

```javascript
import { union,record,string,int32 } from "fable-library/Reflection.js";
import { Union, declare, Record } from "fable-library/Types.js";
export const Person = declare(
    function Test_Person(arg1, arg2) {
        this.Age = arg1 | 0;
        this.Name = arg2; }, Record);
export function Person$reflection() {return record(
        "Test.Person", [], Person,
        () => [["Age", int32], ["Name", string]] );}
export const Contact = declare(
    function Test_Contact(tag, name, ...fields) {
        Union.call(this, tag, name, ...fields); }, Union);
export function Contact$reflection() {return union(
        "Test.Contact", [], Contact, 
        () => [["Email", [string]], ["Phone", [string]]] ); }
```

---

### Saturn - concepts

- Application - start/end of request, configuration, hands to router

- Router - define pipelines to pass requests, parses requests and dispatches to controller

- Controller - contains actions to handle requests, redirect, return views or data

- Views - render templates


---

---?code=full-stack-development/code/app2/src/Shared/Shared.fs&lang=fs
@[1-9](Define Shared namespace and DataTransfer module. Can be accessed at Shared.DataTransfer)
@[12-14](Validation module contains Record for storing error)
@[16-18](ValidationResults is a discriminated union. Can either be Passed or Failed which is a list of ValidationError)
@[20-27](validation functions to return an option of ValidationError )
@[52-59](validation function takes ContactDetails, applies all of validation functions)
@[61-63](pattern matching returns Passed if there are no Some<ValidationError> and Failed of the errors if they exist.)
@[66-70](Define validation response for validation errors returned from server)

--- 

