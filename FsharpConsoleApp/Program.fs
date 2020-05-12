// Learn more about F# at http://fsharp.org

open CoreCmd.CommandExecution
open FsModuleDemo
open FsNamespaceDemo
open ClassDemo

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
        let shout = new FsShoutCommand()
        shout.Hey "model_demo"
        (new FsShoutCommand()).Hey "hello"
        MultipleCall_Execute_MultipleTimes_A

    member __.NamespaceDemo = 
        let speak = new FsSpeakCommand()
        speak.Hey "namespace_demo"

let apply_corecmd argv = 
    let executor = new AssemblyCommandExecutor()
    executor.Execute(argv)

let DoPrint = printfn "Global DoPrint called" 
let DoPrintWithParam param = printfn "Global DoPrintWithParam called %s" param

let ModuleGlobalFnDemo() =
    //MultipleCall_Execute_OnlyOnce
    //MultipleCall_Execute_OnlyOnce // will not be executed

    //MultipleCall_Execute_MultipleTimes_C ()
    //MultipleCall_Execute_MultipleTimes_C () // will be executed
    //MultipleCall_Execute_MultipleTimes_C() // will be executed
    //MultipleCall_Execute_MultipleTimes_C() // will be executed

    //MultipleCall_WillExecute_MultipleTimes "line1"
    //MultipleCall_WillExecute_MultipleTimes "line1"

    let cmd = new FsShoutCommand()
    cmd.Hey "Jon"
    cmd.Hello
    cmd.Hello // will be executed
    

[<EntryPoint>]
let main argv =
    //apply_corecmd argv

    RunClassDemo()
    //ModuleGlobalFnDemo()

    0