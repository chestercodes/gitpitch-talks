
```fsharp
let name = "Chester" 
```

```javascript
export const name = "Chester"; 
```

---

```fsharp
let name = "Chester" 

let addOne arg1 = 
    arg1 + 1
```

```javascript
export const name = "Chester";
export function addOne(arg1) {
  return arg1 + 1;
}
```

---

```fsharp
let name = "Chester" 

let addOne arg1 = 
    arg1 + 1

let aRandomInt = (new System.Random()).Next(5)
```

```javascript
import { randomNext } from "fable-library/Util.js";
export const name = "Chester";
export function addOne(arg1) {
  return arg1 + 1;
}
export const aRandomInt = randomNext(0, 5);
```

---


