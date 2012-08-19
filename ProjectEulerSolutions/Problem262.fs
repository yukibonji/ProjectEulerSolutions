namespace Iit.Fsharp.ProjectEulerSolutions
open System.Linq
open Microsoft.FSharp.Math
open Microsoft.FSharp.Collections
open Iit.Fsharp.Toolkit.Core

type Problem262() =
    let h x y = (5000.0-0.005*(x*x+y*y+x*y)+12.5*(x+y)) * exp(-abs(0.000001*(x*x+y*y)-0.0015*(x+y)+0.7))
    
    let result () = 123I

    interface IProblemSolution with
        member x.ProblemId = 262
        member x.SolutionAlgorithm with get () = result
