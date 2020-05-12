namespace FsharpLib

open UtilityLib

module Say =
    let hello name =
        Print.Separator_______________________________________()
        printfn "Hello %s" name
