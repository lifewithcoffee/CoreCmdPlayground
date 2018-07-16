module ModuleDemo

type ShoutCommand() = class
    member this.Hey name = 
        printfn "Shout: Hey %s" name
end

type YellCommand() =
    member this.Hey name = 
        printfn "Yell: Hi %s" name
        printfn "Yell: another yell"

        let shout = new ShoutCommand()
        shout.Hey "Jerry"

let foo =
    printfn "ModuleDemo foo called"