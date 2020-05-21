namespace FsharpLib

open UtilityLib

module SayFromLib =
    let hello name =
        Print.Separator_______________________________________()
        printfn "Hello %s from SayFromLib" name
