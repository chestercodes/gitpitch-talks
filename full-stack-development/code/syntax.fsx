// adapted from https://fsharpforfunandprofit.com/posts/fsharp-in-60-seconds/
let myInt = 5
let myFloat = 3.14
let myString = "hello"

let myList =  [ 1; 2 ]
let myArray = [| 1; 2; 3; |]
let mySecondList = [
   1
   2
]
let areSame = myList = mySecondList // true


// The "let" keyword also defines a named function.
let square x = x * x  // Note that no parens are used.
let nine = square 3   // Now run the function. Again, no parens.

// can specify types and spread declaration over lines
let add    // function name
   (x:int) // parameter 1
   (y:int) // parameter 2
   : int   // return type
  = 
  x + y // function body 

let three = add 1 2

// pipe operator can be used with anonymous function
let evens list =
   list 
   |> List.filter (fun x -> x % 2 = 0) 

// Tuple types are pairs, triples, etc. Tuples use commas.
let twoTuple = 1, 2
let threeTuple = ("a", 2, true)