module FunctionDemo

open FsModuleDemo
open UtilityLib

// [notes] parameterless function declared with no brackets
//       - treated as a varialbe (a variable will only be evaluated once)
let DoPrint = printfn "Global DoPrint called"

// [notes] parameterless function decalred with brackets
//       - not treated as a variable, but a function definition
let DoPrint2() = printfn "Global DoPrint2 called"

// [notes] will always be treated as a function definition since it has a parameter
let DoPrintWithParam param = printfn "Global DoPrintWithParam called %s" param

type FnCommand() =
    member _.Demo() =
        Print.Separator_______________________________________()

        DoPrint     // [notes] will be called for the first evaluation
        DoPrint     // [notes] will not be called since, as a variable, it has been evaluated

        DoPrint2()  // [notes] will always be called, as it's a function
        DoPrint2()  // [notes] will always be called, as it's a function

        DoPrintWithParam "bla"  // [notes] will always be called, as it's a function
        DoPrintWithParam "bla"  // [notes] will always be called, as it's a function

