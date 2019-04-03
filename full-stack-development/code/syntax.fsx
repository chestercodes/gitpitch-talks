// https://fsharpforfunandprofit.com/posts/fsharp-in-60-seconds/
let myInt = 5
let myFloat = 3.14
let myString = "hello"

let twoToFive = [2;3;4;5]        // Square brackets create a list.
let oneToFive = 1 :: twoToFive   // :: creates list with new 1st element
// The result is [1;2;3;4;5]
let zeroToFive = [0;1] @ twoToFive   // @ concats two lists

// The "let" keyword also defines a named function.
let square x = x * x          // Note that no parens are used.
square 3                      // Now run the function. Again, no parens.

// can specify types and spread declaration over lines
let add    // function name
   (x:int) // parameter 1
   (y:int) // parameter 2
   : int   // return type
  = 
  x + y // function body 

// to define a multiline function, just use indents. No semicolons needed.
let evens list =
   let isEven x = x % 2 = 0     // Define "isEven" as an inner ("nested") function
   List.filter isEven list    // List.filter is like LINQ's Where
evens oneToFive               // Now run the function

// pipe operator can be used with anonymous function
let evens2 list =
   list 
   |> List.filter (fun x -> x % 2 = 0) 

// Tuple types are pairs, triples, etc. Tuples use commas.
let twoTuple = 1,2
let threeTuple = "a",2,true

// The printf/printfn functions are similar to Console.Write/WriteLine functions.
printfn "Printing an int %i, a float %f, a bool %b" 1 2.0 true
printfn "A string %s, and something generic %A" "hello" [1;2;3;4]

// immutable by default, mutability requires mutable keyword
let mutable counter = 0
// and special assignment operator
counter <- counter + 1

