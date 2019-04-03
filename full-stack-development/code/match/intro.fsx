
// binding values to expressions with let
let (first, second) = functionThatReturnsTuple arg

// in function parameters
let someFunction (arg1first, arg1second) arg2 =
     // first parameter, is tuple with items matched to arg1first and arg1second  
    arg1first * arg1second * arg2 

// branching using the match..with syntax
match something with 
| pattern1 -> expression1
| pattern2 -> expression2
| pattern3 -> expression3