namespace FsNamespaceDemo

open UtilityLib

// [notes] module is actually a static class, which cannot be reflected by CoreCmd
module FsSayCommand =
    let hey name =
        Print.Separator_______________________________________()
        printfn "Hello %s" name

type FsSpeakCommand() = class
    member __.Hey name = 
        Print.Separator_______________________________________()
        printfn "Speak: Hey %s, count = 1" name
end
