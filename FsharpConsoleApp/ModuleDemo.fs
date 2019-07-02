module ModuleDemo

type ShoutCommand() = class
    member __.Hey name = 
        printfn "Shout: Hey %s" name

    member __.Hello =
        printfn "Shout: Hello without param"
end

type YellCommand() =
    member __.Hey name = 
        printfn "Yell: Hi %s" name
        printfn "Yell: another yell"

        let shout = new ShoutCommand()
        shout.Hey "Jerry"

let MultipleCall_Execute_MultipleTimes_A x =
    printfn "hello a, x: %s" x

printfn "ModuleDemo loaded (statement is in the middle of this file)"

let MultipleCall_Execute_OnlyOnce =
    printfn "hello b"

let MultipleCall_Execute_MultipleTimes_C () =
    printfn "hello c"

printfn "ModuleDemo loaded (statement is at the end of this file)"
