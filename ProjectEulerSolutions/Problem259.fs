namespace Iit.Fsharp.ProjectEulerSolutions
open System.Linq

type Problem259() =
    /// enumerates partitions of a list of non-empty left and right parts
    let rec partitions list =
        let attach head (left, right) = (head::left, right)
        seq {
            match list with
                | [a] -> ()
                | [] -> ()
                | [a;b] -> yield ([a], [b])
                | h::t -> 
                    yield ([h], t)
                    yield! partitions t |> Seq.map (attach h)
        }

    /// creates a number from a list of digits
    let number list =
        let rec cat n list =
            match list with
                | [] -> n
                | h::t -> cat (10 * n + h) t
        cat 0 list

    let result () = 123

    interface IProblemSolution with
        member x.ProblemId = 5
        member x.SolutionAlgorithm with get () = result