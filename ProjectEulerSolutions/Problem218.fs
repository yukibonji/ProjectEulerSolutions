namespace Iit.Fsharp.ProjectEulerSolutions
open System.Linq
open Microsoft.FSharp.Math
open Microsoft.FSharp.Collections
open Iit.Fsharp.Toolkit.Core

type Problem218() =
    let primitiveTriangles u = seq {u-1..-2..1} |> Seq.filter (fun x -> gcd u x = 1)

    let isSquare n = 
        sqrt (float n) % 1.0 = 0.0 

    let perfectTriangles u =
        let longU = int64 u
        let getTriple v = (longU*longU - v*v, 2L*longU*v, longU*longU + v*v)
        primitiveTriangles u
        |> Seq.map int64
        |> Seq.filter (fun x -> isSquare (x*x + longU*longU))
        |> Seq.map getTriple
    
    let notSuperPerfectTriangles list =
        list |> Seq.filter (fun (x,y,_) -> x*y % 6L <> 0L || x*y % 28L <> 0L)
    
    let goodTriangles n =
        perfectTriangles n
        |> notSuperPerfectTriangles
        |> Seq.filter (fun (_, _, z) -> z <= 10000000000000000L)

    let result () = 
        bigint (
            seq {1..10000000}
            |> PSeq.collect goodTriangles
            |> PSeq.length)

        

    interface IProblemSolution with
        member x.ProblemId = 218
        member x.SolutionAlgorithm with get () = result