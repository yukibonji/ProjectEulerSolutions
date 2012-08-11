namespace Iit.Fsharp.Toolkit

open System.Threading.Tasks
module Tpl =
    type TaskBuilder() =
        member this.Bind (f : Task<_>, rest) = rest f.Result
        member this.Bind (f : Task, rest) = f.RunSynchronously(); rest () 

        member this.Delay f = f()
        
        member this.Return f = new Task<_>(fun () -> f())
        member this.Return f = new Task(fun () -> f())

        member this.ReturnFrom value = value
    
    let task = new TaskBuilder()

    let compute () = 3
    let run = task { return fun () -> () }

    let compute3 = task {
        do! run
        return compute
    }

    let tryVal = task {
        let! three = compute3 

        return! three
    }
    
    let test = 34
