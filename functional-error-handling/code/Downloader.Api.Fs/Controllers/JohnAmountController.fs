namespace Downloader.Api.Fs.Controllers

open System
open Microsoft.AspNetCore.Mvc

module JohnAmount =
    open Downloader.Api.Shared

    type DownloadingError = SftpUnauthorized | FileNotPresent
    
    type JohnAmountError = 
        | DownloadingError of DownloadingError
        | FileDoesNotParse
    
    type PersonAmount = { Name: string; Amount: decimal }

    let download fileName =
        match fileName with
        | FileNames.UnauthorisedSftp -> Error SftpUnauthorized
        | FileNames.FileMissingOnSftp -> Error FileNotPresent
        | FileNames.FileDoesntParse -> Ok FileContent.InvalidFile
        | _ -> Ok FileContent.ValidFile
    
    let parse (content: string) =
        let parseLine (line: string) = 
            match line.Split(',') with
            | [| name; amount |] -> 
                match Decimal.TryParse amount with
                | (true, result) -> Some { Name = name ; Amount = result }
                | (false, _) -> None
            | _ -> None
        
        content.Split('\n') 
        |> Array.fold (fun agg el ->
            let parsed = parseLine el
            match parsed with
            | Some personAmount -> agg |> Result.map (fun lines -> lines @ [personAmount])
            | None -> Error FileDoesNotParse
        ) (Ok [])


open JohnAmount

[<Route("api/[controller]")>]
[<ApiController>]
type JohnAmountController () =
    inherit ControllerBase()

    [<HttpGet("{fileName}")>]
    member this.Get(fileName:string) =
        
        download fileName |> Result.mapError DownloadingError
        |> Result.bind parse
        |> Result.map (fun personAmounts ->
            personAmounts 
            |> List.filter (fun x -> x.Name = "John")
            |> List.sumBy (fun x -> x.Amount)
        )
        |> (fun res ->
                match res with 
                | Ok amount -> JsonResult(amount) :> ActionResult
                | Error error ->
                    match error with
                    | DownloadingError downloadingError -> 
                        match downloadingError with
                        | SftpUnauthorized -> this.Unauthorized() :> ActionResult
                        | FileNotPresent -> this.NotFound() :> ActionResult
                    | FileDoesNotParse -> this.UnprocessableEntity() :> ActionResult
        )
