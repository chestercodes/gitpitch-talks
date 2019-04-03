
















// requires R version less than 3.5 - https://www.stats.bris.ac.uk/R/bin/windows/base/old/3.4.4/
// seems to be bug in R.NET

// below lines only apply to running as script
// #I "./packages/RProvider.1.1.21"
// #load "RProvider.fsx"

open System.IO
open RDotNet
open RProvider
open RProvider.graphics
open RProvider.grDevices

[<Measure>] type GBP
[<Measure>] type year

[<Measure>] type percent

let fromPercentage (percentage: float<percent>): float =
    float percentage / 100.0

let toPercentage (whole: float): float<percent> =
    whole * 100.0<percent>


let csv = """

Bank, Apr(%), WelcomeGift
1,    3.1,    200
2,    4.2,    100
3,    5.3,    0

"""

type BankInfo = { Bank: int; Gift: float<GBP>
                  Apr: float<percent/year> }

let csvLineToBankInfo (csvLine: string) =
    let tryFloat (s: string) = try Some (float (s.Trim())) with | _ -> None
    let tryInt (s: string) = try Some (int (s.Trim())) with | _ -> None

    match csvLine.Split ',' with
    | [|c1; c2; c3|] -> 
        match tryInt c1, tryFloat c2, tryFloat c3 with
        | Some bank, Some apr, Some gift -> 
            Some { Bank = bank
                   Apr = apr * 1.0<percent/year>
                   Gift = gift * 1.0<GBP> }
        | _ -> None            
    | _ -> None            

let compoundInterest
    (rate: float<percent/year>)
    (years: float<year>) : float<percent> =
    // 5% over 2 years is 10.25% increase- ((1.05 ^ 2) * 100)%
    let percentIncrease = 100.0<percent> + (rate * 1.0<year>)
    (fromPercentage percentIncrease) ** (float years) 
    |> toPercentage

let calculateAmount initialBalance bankInfo years: float<GBP> =
    let compoundedInterest = compoundInterest bankInfo.Apr years
    let interestMultiplier = fromPercentage compoundedInterest
    (initialBalance + bankInfo.Gift) * interestMultiplier

[<EntryPoint>]
let main argv = 
    
    let bankInfos = 
        csv.Split('\n')
        |> Array.toList
        |> List.choose csvLineToBankInfo

    let initialBalance = 1000.0<GBP>
    let numberOfYears = 10.0<year>
    let amountsAfter10Years = 
        bankInfos
        |> List.map (fun x -> 
            x, calculateAmount initialBalance x numberOfYears)
        |> List.sortBy snd

    for am in amountsAfter10Years do
        let bank = (fst am).Bank
        let amount = (snd am)
        printfn "Bank %i amount %f " bank amount



    let xValues = [0..10] |> List.map float
    
    let overYears = 
        bankInfos
        |> List.map (fun bi ->
            let yValues = 
                xValues 
                |> List.map (fun x -> 
                    calculateAmount initialBalance bi (x * 1.0<year>))
            bi, yValues
        )

    let maxY = overYears 
               |> List.map snd 
               |> List.collect id 
               |> List.max

    let colours = ["red" ; "blue" ; "green"]
    let yLim = [float initialBalance; float maxY]
    let xLim = [0.0 ; (List.max xValues)]

    let dir = new DirectoryInfo(System.Reflection.Assembly.GetExecutingAssembly().Location)
    let path = Path.Combine(dir.Parent.Parent.Parent.Parent.Parent.Parent.FullName, "assets", "img", @"banks.png")
    
    R.png(filename=path, height=500, width=500, bg="white") |> ignore
    
    namedParams [   
        "x", box [1]
        "ylim", box yLim 
        "xlim", box xLim 
        "xlab", box ""
        "ylab", box ""
        ]
    |> R.plot |> ignore
    
    for data in overYears do
        let bankInfo = fst data
        let colour = colours.[bankInfo.Bank - 1]
        
        namedParams [
            "x", box xValues;
            "y", box (snd data);
            "col", box colour ]
        |> R.lines  |> ignore

        let text = sprintf "Bank %i -> %.1f%% , £%.0f" 
                    bankInfo.Bank bankInfo.Apr bankInfo.Gift
        
        namedParams [
            "x", box 6
            "y", box (1300 - 80 * bankInfo.Bank)
            "legend", box text 
            "col", box colour 
            "fill", box colour ]
        |> R.legend |> ignore

    R.title(main="Banks", xlab="Years", ylab="Amount") |> ignore

    R.dev_off () |> ignore
    
    0 // return an integer exit code
    
