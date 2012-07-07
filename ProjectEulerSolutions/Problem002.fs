namespace Iit.Fsharp.ProjectEulerSolutions
open System.Linq

type Problem002() =
    let rec fib n =
        match n with
            | 0 -> 1
            | 1 -> 2
            | n -> fib (n-1) + fib (n-2)
    
    let fibUntil maxValue =
        let rec generate i =
            let fibValue = fib i
            match fibValue with
                | x when x <= maxValue -> x :: generate (i+1)
                | _ -> []
        generate 0
    
    let even x = (x % 2) = 0
    let result () =
        fibUntil 4000000
        |> List.filter even
        |> List.sum

    interface IProblemSolution with
        member x.ProblemId = 2
        member x.SolutionAlgorithm with get () = result