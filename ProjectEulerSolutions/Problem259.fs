namespace Iit.Fsharp.ProjectEulerSolutions
open System.Linq
open Microsoft.FSharp.Math
open Microsoft.FSharp.Collections
open Iit.Fsharp.Toolkit.Core

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
        let rec cat = cache HashIdentity.Structural (fun n list ->
            match list with
                | [] -> n
                | h::t -> cat (10N * n + h) t)
        cat 0N list

    let rec reachableNumbers = 
        let reach reachableNumbers operator (left, right) = 
            seq {
                for l in reachableNumbers left do
                for r in reachableNumbers right do
                let tryExecute f =
                    fun x y ->
                        try Some(f x y) with | _ -> None
                yield tryExecute operator l r
            } |> PSeq.choose id
        let result list = 
            seq {          
                yield number list
                for (left, right) in partitions list do
                    yield! reach reachableNumbers (+) (left, right) 
                    yield! reach reachableNumbers (*) (left, right) 
                    yield! reach reachableNumbers (/) (left, right) 
                    yield! reach reachableNumbers (-) (left, right) 
            } |> PSeq.distinct |> PSeq.toList
        cache HashIdentity.Structural result


    let reachableIntegers list =
        reachableNumbers list
        |> PSeq.filter (fun value -> value.IsPositive && value.Denominator = 1I)
        |> PSeq.map (fun value -> value.Numerator)
        |> PSeq.reduce (+)

    let result () = reachableIntegers [1N..9N]

    interface IProblemSolution with
        member x.ProblemId = 259
        member x.SolutionAlgorithm with get () = result