module ClassDemo

open FsModuleDemo
open System
open UtilityLib

type CustomerName(firstName, lastName, birthYear) = 

    // [notes] 'this' can be replace by any string, see 'jackie_chan.Info' below
    member this.FirstName = firstName
    member __.LastName = lastName  
    member _.age = DateTime.Now.Year - birthYear
    member jackie_chan.Info = 
        Print.Separator_______________________________________()
        printfn "Full name: %s %s; Age: %d" jackie_chan.FirstName jackie_chan.LastName jackie_chan.age

// [notes] if need to run as corecmd command by: do class demo
//         the method Demo() must have brackets "()" declared
type ClassCommand() = 
    member __.Demo() = 
        Print.Separator_______________________________________()
        let customerName = new CustomerName("John", "Boni", 1981)
        customerName.Info        // [notes] will be called as get_Info() as Info does not have "()" declared
