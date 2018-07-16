// Learn more about F# at http://fsharp.org

open System
open CoreCmd

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    let cmd = new CoreCmd.CommandExecutor()
    cmd.Execute(argv)
    0 // return an integer exit code
