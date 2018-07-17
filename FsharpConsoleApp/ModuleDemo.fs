module ModuleDemo

type ShoutCommand() = class
    member __.Hey name = 
        printfn "Shout: Hey %s" name
end

type YellCommand() =
    member __.Hey name = 
        printfn "Yell: Hi %s" name
        printfn "Yell: another yell"

        let shout = new ShoutCommand()
        shout.Hey "Jerry"

let foo =
    printfn "ModuleDemo foo called"