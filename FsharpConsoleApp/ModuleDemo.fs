module FsModuleDemo

open UtilityLib

type FsShoutCommand() = class
    member __.Hey name = 
        Print.Separator_______________________________________()
        printfn "Shout: Hey %s" name

end

printfn "ModuleDemo loaded (statement is in the middle of this file)"

type FsYellCommand() =
    member __.Hey name = 
        Print.Separator_______________________________________()
        printfn "Yell: Hi %s" name
        printfn "Yell: another yell"

        let shout = new FsShoutCommand()
        shout.Hey "Jerry"

printfn "ModuleDemo loaded (statement is at the end of this file)"
