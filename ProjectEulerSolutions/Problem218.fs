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
        seq {1I..u-1I}
        |> Seq.map (fun v -> (u*u - v*v, 2I*u*v, u*u + v*v))
        |> Seq.map swap

    let triples = 
        seq {2I..10000I}
        |> PSeq.collect triplesFor
    
    let perfectTriangles =
        let generate (x, y, z) = (x*x - y*y, 2I*x*y, x*x + y*y)
        let primitive (x, y, z) = gcd x y = 1I && gcd y z = 1I && gcd x z = 1I
        triples
        |> PSeq.map generate 
        |> PSeq.filter primitive
        |> PSeq.filter (fun (_, _, z) -> z <= 10000000000000000I)


    let notSuperPerfectTriangles list =
        list
        |> PSeq.filter (fun (x,y,_) -> (x*y/2I) % 6I <> 0I || (x*y/2I) % 28I <> 0I)
    
    let goodTriangles =
        perfectTriangles
        |> notSuperPerfectTriangles

    let result () = bigint (goodTriangles |> Seq.length)

    interface IProblemSolution with
        member x.ProblemId = 218
        member x.SolutionAlgorithm with get () = result