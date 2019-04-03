let fizzbuzz n =
  match n with
  | x when x % 15 = 0 -> "FizzBuzz"
  | x when x % 3  = 0 -> "Fizz"
  | x when x % 5  = 0 -> "Buzz"
  | x                 -> x.ToString()

// match test-expression with
// | pattern1 [ when condition ] -> result-expression1
// | pattern2 [ when condition ] -> result-expression2
// | ...
