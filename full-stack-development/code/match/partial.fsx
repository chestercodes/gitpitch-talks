let (|DividesBy|_|) modN n = if n % modN = 0 then Some n else None 
let fizzbuzz3 n = 
  match n with
  | DividesBy 15 _ -> "FizzBuzz"
  | DividesBy 3  _ -> "Fizz"
  | DividesBy 5  _ -> "Buzz"
  | x              -> x.ToString()