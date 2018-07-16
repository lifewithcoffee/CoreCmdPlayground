namespace NamespaceDemo

module SayCommand =
    let hey name =
        printfn "Hello %s" name

type SpeakCommand() = class
    member this.Hey name = 
        printfn "Speak: Hey %s" name
end
