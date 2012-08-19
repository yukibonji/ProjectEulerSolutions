namespace Iit.Fsharp.ProjectEulerSolutions
open System.Linq
open Iit.Fsharp.Toolkit.Core

type Problem0045 () =
    let triangle = seq 
    let result () = 123I

    interface IProblemSolution with
        member x.ProblemId = 45
        member x.SolutionAlgorithm with get () = result