﻿// Learn more about F# at http://fsharp.org

open CoreCmd
open ModuleDemo
open NamespaceDemo

type NoiseCommand() =
    // from: http://developer.51cto.com/art/200908/143854.htm
    member __.Beep n =
        let container = Array.create (n+1) 0  // create an array with the size of n+1, all initialized to 0
        printfn "%A" container
        let rec loop acc = function  
            |[] -> List.rev acc  
            |hd::tl ->   
                if container.[hd] =1 then   
                    loop acc tl  
                else 
                    for j in [hd .. hd .. n] do 
                        container.[j] <- 1  
                    loop (hd::acc) tl      
        let result = loop [] [2 .. n]  
        printfn "%A" result

    member __.ModelDemo = 
        let shout = new ShoutCommand()
        shout.Hey "model_demo"
        (new ShoutCommand()).Hey "hello"
        foo

    member __.NamespaceDemo = 
        let speak = new SpeakCommand()
        speak.Hey "namespace_demo"

let apply_corecmd argv = 
    let executor = new CommandExecutor()
    executor.Execute(argv)
    
[<EntryPoint>]
let main argv =
    apply_corecmd argv
    0 // return an integer exit code