// Learn more about F# at http://fsharp.org

open System
open CoreCmd
open ModuleDemo
open NamespaceDemo

//let rltest = 
//    let yell = new YellCommand()
//    yell.Hey "Joe"

type Noise() =
    member this.beep sound =
        printfn "Beep %s %s" sound sound

    member this.model_demo = 
        let shout = new ShoutCommand()
        shout.Hey "model_demo"
        (new ShoutCommand()).Hey "hello"
        foo

    member this.namespace_demo = 
        let speak = new SpeakCommand()
        speak.Hey "namespace_demo"

let apply_corecmd argv = 
    let executor = new CommandExecutor()
    executor.Execute(argv)

[<EntryPoint>]
let main argv =
    let n = new Noise()
    //n.beep "hello"
    n.model_demo

    //apply_corecmd argv
    0 // return an integer exit code