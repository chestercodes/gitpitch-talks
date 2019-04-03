let (|DividesBy3|DividesBy5|DividesBy15|Other|) n = 
  match n with
  | x when x % 15 = 0 -> DividesBy15
  | x when x % 3  = 0 -> DividesBy3
  | x when x % 5  = 0 -> DividesBy5
  | _                 -> Other

let fizzbuzz n = 
  match n with
  | DividesBy15 -> "FizzBuzz"
  | DividesBy3  -> "Fizz"
  | DividesBy5  -> "Buzz"
  | Other       -> n.ToString()