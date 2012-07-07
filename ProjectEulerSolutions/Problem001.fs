namespace Iit.Fsharp.ProjectEulerSolutions
open System.Linq

type Problem001() =
    let numbersDividableBy3or5 input = input |> Seq.filter (fun item -> (item % 3 * item % 5) = 0)
    let solution input = numbersDividableBy3or5 input |> Seq.sum
    
    let result maxValue () = solution (System.Linq.Enumerable.Range (1, maxValue))
    interface IProblemSolution with
        member x.ProblemId = 1
        member x.SolutionAlgorithm with get () = result 999