[<Measure>] type GBP
[<Measure>] type Q
let alreadyOwns = 10.4<Q>     // float<Q>
let fundPrice = 2.4<GBP/Q>    // float<GBP/Q>
let availableCash = 14.0<GBP> // float<GBP>

let (|HasNoCash|PriceIsInvalid|Valid|) (cash, price) =
    match () with
    | _ when cash <= 0.0<GBP> -> HasNoCash
    | _ when price <= 0.0<GBP/Q> -> PriceIsInvalid
    | _ -> Valid

let maxCanBuy cash price =  
    match cash, price with
    | HasNoCash | PriceIsInvalid -> 0.0<Q>
    | Valid -> cash / price

let total = alreadyOwns + maxCanBuy availableCash fundPrice  // float<Q>