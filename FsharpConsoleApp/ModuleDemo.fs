module FsModuleDemo

open UtilityLib

type FsShoutCommand() = class
    member __.Hey name = 
        Print.Separator_______________________________________()
        printfn "Shout: Hey %s" name

    member __.Hello =
        Print.Separator_______________________________________()
        printfn "Shout: Hello without param"
end

type FsYellCommand() =
    member __.Hey name = 
        Print.Separator_______________________________________()
        printfn "Yell: Hi %s" name
        printfn "Yell: another yell"

        let shout = new FsShoutCommand()
        shout.Hey "Jerry"

let MultipleCall_Execute_MultipleTimes_A x =
    Print.Separator_______________________________________()
    printfn "hello a, x: %s" x

printfn "ModuleDemo loaded (statement is in the middle of this file)"

let MultipleCall_Execute_OnlyOnce =
    Print.Separator_______________________________________()
    printfn "hello b"

let MultipleCall_Execute_MultipleTimes_C () =
    Print.Separator_______________________________________()
    printfn "hello c"

printfn "ModuleDemo loaded (statement is at the end of this file)"
