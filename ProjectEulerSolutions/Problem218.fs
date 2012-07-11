namespace Iit.Fsharp.ProjectEulerSolutions
open System.Linq
open Microsoft.FSharp.Math
open Microsoft.FSharp.Collections
open Iit.Fsharp.Toolkit.Core

type Problem218() =
    let triplesFor u =
        let swap (x, y, z) = 
            match (x, y, z) with
                | (x, y, z) when x < y -> (y, x, z)
                | (x, y, z) -> (x, y, z)
        seq {1..u-1}
        |> Seq.map (fun v -> (u*u - v*v, 2*u*v, u*u + v*v))
        |> Seq.map swap

    let triples =
        seq {2..5000}
        |> PSeq.collect triplesFor
        |> List.ofSeq
    
    let perfectTriangles =
        let toLong (x, y, z) = (int64 x, int64 y, int64 z)
        let generate (x, y, z) = (x*x - y*y, 2L*x*y, x*x + y*y)
        let primitive (x, y, z) = gcd x y = 1L  
        triples |> PSeq.map toLong |> PSeq.map generate |> PSeq.filter primitive

    let notSuperPerfectTriangles list =
        list
        |> PSeq.filter (fun (x,y,_) -> (x*y/2L) % 6L <> 0L || (x*y/2L) % 28L <> 0L)
    
    let goodTriangles =
        perfectTriangles
        |> notSuperPerfectTriangles
        |> PSeq.filter (fun (_, _, z) -> z <= 10000000000000000L)

    let result () = 
        bigint (goodTriangles |> PSeq.length)

    interface IProblemSolution with
        member x.ProblemId = 218
        member x.SolutionAlgorithm with get () = result