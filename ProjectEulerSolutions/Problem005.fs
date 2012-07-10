namespace Iit.Fsharp.ProjectEulerSolutions
open System.Linq

type Problem005() =
    let primes = [2; 3; 5; 7; 11; 13; 17; 19]

    let factor number =
        let rec reduce sieve number = 
            match sieve with
                | head::tail when (number % head) = 0 -> head :: (reduce sieve (number / head))
                | head::tail -> reduce tail number
                | [] -> [number]
        reduce primes number |> List.filter ((<) 1) |> Seq.countBy id |> List.ofSeq
        
    let primeFactorsWithOccurences list = 
        list
        |> List.map factor
        |> List.collect id
        |> Seq.groupBy (fun (x, y) -> x)
        |> List.ofSeq
        |> List.map (fun (x, y) -> y |> Seq.maxBy (fun (a, b) -> b))

    let kgv list = 
        let rec pow (basis, exponent) = 
            match exponent with
                | 0 -> 1
                | n -> basis * (pow (basis, exponent - 1))
        let rec multiply list = 
            match list with
                | head::tail -> pow head * (multiply tail)
                | [] -> 1
        multiply (primeFactorsWithOccurences list)

    let result () = bigint(kgv [2..20])

    interface IProblemSolution with
        member x.ProblemId = 5
        member x.SolutionAlgorithm with get () = result