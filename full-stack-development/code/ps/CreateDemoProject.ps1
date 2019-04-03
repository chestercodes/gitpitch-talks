$workDir = pwd
$tempDir = "C:\temp\IntroToFSharp"
[System.IO.Directory]::CreateDirectory($tempDir)
cd $tempDir

$demoDir = "Demo"
$demoName = "Demo"
dotnet new console -lang "F#" --name $demoName --output $demoDir

dotnet new sln --name IntroToFSharp
dotnet sln add .\$demoDir\$demoName`.fsproj

cd $demoDir
dotnet add package FSharp.Text.RegexProvider --version 1.0.0 
dotnet add package SQLProvider --version 1.1.58

dotnet restore

cd $workDir


[System.IO.File]::AppendAllText("$tempDir/$demoDir/Program.fs", "

open FSharp.Text.RegexProvider

let [<Literal>] pattern = @""(?<AreaCode>^\d{5})\s(?<Rest>\d{6}$)""  

let printPhoneNumberInfo input =
    // print either of these depending on match
    printfn ""Area code is %s, rest of number is %s"" ""TODO"" ""TODO""
    printfn ""Input '%s' is not phone number"" input

[<EntryPoint>]
let main argv =
    
    printPhoneNumberInfo ""01234 567890""
    printPhoneNumberInfo ""sausage""
    
    System.Console.ReadKey() |> ignore
    0

")