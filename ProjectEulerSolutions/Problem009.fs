namespace Iit.Fsharp.ProjectEulerSolutions
open System.Linq
open Iit.Fsharp.Toolkit.Core

type Problem009 () =
      
    let pythagoreans maxM = 
        seq {
            for m in seq {1I..maxM} ->
                seq {
                    for n in seq {1I..m-1I} ->
                        (m*m - n*n, 2I*m*n, m*m + n*n)
                }
        }
        |> Seq.collect id
        |> Seq.filter (fun (x, y, z) -> x+y+z = 1000I)

    let (x, y, z) = Seq.head (pythagoreans 1000I)
    let result () = x*y*z

    interface IProblemSolution with
        member x.ProblemId = 9
        member x.SolutionAlgorithm with get () = result