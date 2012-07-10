namespace Iit.Fsharp.ProjectEulerSolutions
open System.Linq
open Microsoft.FSharp.Math
open Microsoft.FSharp.Collections

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
                | h::t -> cat (10N * n + h) t
        cat 0N list

    let rec reachableNumbers list = 
        let reach reachableNumbers operator (left, right) = 
            seq {
                for l in reachableNumbers left do
                for r in reachableNumbers right do
                yield operator l r
            }
        seq {          
            yield number list 
            for (left, right) in partitions list do
                yield! reach reachableNumbers (+) (left, right) 
                yield! reach reachableNumbers (*) (left, right) 
                yield! reach reachableNumbers (/) (left, right) 
        } |> Seq.distinct


    let reachableIntegers list =
        reachableNumbers list
        |> Seq.filter (fun value -> value.IsPositive && value.Denominator = 1I)
        |> Seq.map (fun value -> value.Numerator)
        |> Seq.sum

    let result () = reachableIntegers [1N..9N]

    interface IProblemSolution with
        member x.ProblemId = 259
        member x.SolutionAlgorithm with get () = result