open System.IO
open System
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.DependencyInjection
open FSharp.Control.Tasks.V2
open Giraffe
open Saturn

open Shared.Validation
open Shared.DataTransfer
open Shared.Domain

let tryGetEnv = System.Environment.GetEnvironmentVariable >> function null | "" -> None | x -> Some x

let publicPath = Path.GetFullPath "../Client/public"

let port = "SERVER_PORT" |> tryGetEnv |> Option.map uint16 |> Option.defaultValue 8085us

let contactPost next (ctx: HttpContext) =
    task {
        try
            let! contactDetails =  ctx.BindModelAsync<UnvalContactDetails>()
            System.Threading.Thread.Sleep(500)
            match validateContactDetails contactDetails.email contactDetails.phone with
            | Passed -> 
                return! json ({ Id = Guid.NewGuid().ToString() }) next ctx
            | Failed errors -> 
                return! Response.unprocessableEntity ctx ({ errors = errors })        
        with 
            | ex -> 
                return! Response.badRequest ctx ex.Message
    }

let webApp = router {
    post "/api/contact" contactPost
}

let app = application {
    url ("http://0.0.0.0:" + port.ToString() + "/")
    use_router webApp
    memory_cache
    use_static publicPath
    use_json_serializer(Thoth.Json.Giraffe.ThothSerializer())
    use_gzip
}

run app
