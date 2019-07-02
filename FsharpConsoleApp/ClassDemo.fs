module ClassDemo

open ModuleDemo
open System

type CustomerName(firstName, lastName, birthYear) = 
    member this.FirstName = firstName
    member this.LastName = lastName  
    member this.age = DateTime.Now.Year - birthYear
    member this.displayInfo = 
        printfn "Full name: %s %s; Age: %d" this.FirstName this.LastName this.age

let RunClassDemo () = 
    let customerName = new CustomerName("John", "Boni", 1981)
    customerName.displayInfo
