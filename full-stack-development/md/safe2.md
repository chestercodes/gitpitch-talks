
F#

```fsharp
let name = "Chester" 
```

JavaScript

```javascript
export const name = "Chester"; 
```

---

F#

```fsharp
let name = "Chester" 

let addOne arg1 = 
    arg1 + 1
```

JavaScript

```javascript
export const name = "Chester";
export function addOne(arg1) {
  return arg1 + 1;
}
```

---

F#

```fsharp
let name = "Chester" 

let addOne arg1 = 
    arg1 + 1

let aRandomInt = (new System.Random()).Next(5)
```

JavaScript

```javascript
import { randomNext } from "fable-library/Util.js";
export const name = "Chester";
export function addOne(arg1) {
  return arg1 + 1;
}
export const aRandomInt = randomNext(0, 5);
```

---

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

Native JavaScript

```fsharp

let mutable private arr = [| 1; 2; 3; 4; 5 |]

let str = Fable.Import.JS.JSON.stringify arr 

Fable.Import.JS.console.log("Hello world!")
                                                                                                   //
```

```javascript

let arr = new Int32Array([1, 2, 3, 4, 5]);

export const str = JSON.stringify(arr);

console.log("Hello world!");
                                                                                                   //
```
