open System.Net
open System

let fetchUrlAsync url =        
    async {                             
        let req = WebRequest.Create(Uri(url)) 
        use! resp = req.AsyncGetResponse()
        use stream = resp.GetResponseStream() 
        use reader = new IO.StreamReader(stream) 
        let html = reader.ReadToEnd() 
        printfn "finished downloading %s" url 
        }

let port = 29701
let controller = "johnamountsafeasync"
//let controller = "johnamountsafewait"

#time
[1..50]
|> List.map (fun x -> sprintf "http://localhost:%i/api/%s/num%i" port controller x)
|> List.map fetchUrlAsync  // make a list of async tasks
|> Async.Parallel          // set up the tasks to run in parallel
|> Async.RunSynchronously  // start them off
|> ignore
#time