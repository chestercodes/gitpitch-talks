type Address = { Line1: string; Line2: string; PostCode: string }

type Person =  { Name: string
                 Age: int
                 Address: Address }

let john =     { Name = "John"; Age = 30
                 Address = { Line1 = "1 lane"; Line2 = "1 street"
                             PostCode = "BS11BS"} }

let sameJohn = { Name = "John"; Age = 30
                 Address = { Line1 = "1 lane"; Line2 = "1 street"
                             PostCode = "BS11BS"} }

printfn "Johns are equal - %b" (john = sameJohn)

let copyJohn = { john with Address = 
                           { john.Address with Line1 = "2 lane"} }