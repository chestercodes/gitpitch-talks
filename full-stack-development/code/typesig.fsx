// string * int
let nameAndAgeTuple = "Chester", 29

// int -> string
let printAge intArg =
    sprintf "Person is %i" intArg

// (string -> unit) -> string -> string
let doAndAddALittleSomething action stringArg =
    action stringArg
    sprintf "%s something" stringArg

// C#   Func< int,   string >
// F#         int -> string

// C#   Func< Action<string>,     string,  string >
// F#         (string -> unit) -> string -> string
