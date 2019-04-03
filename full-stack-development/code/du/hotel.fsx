// define type
type RoomName =         // type TypeName =
    | Number of int     //     | case1 of type1
    | Name of string    //     | case2 of type2
    | Reception         //     | case3

// construct values
let room5 =      RoomName.Number 5   // let typeVal1 = case1 value1
let grandSuite = Name "Grand Suite"  // let typeVal2 = case2 value2
let reception =  Reception           // let typeVal3 = case3

let printRoomName roomName =
    match roomName with
    | Number number -> printfn "Room %i" number
    | Name name     -> printfn "Room - '%s'" name
    | Reception     -> printfn "Reception"

printRoomName room5       // prints "Room 5"
printRoomName grandSuite  // prints "Room - 'Grand Suite'"
printRoomName reception   // prints "Reception"