namespace Iit.Fsharp.ProjectEulerSolutions
open System.Linq

type Problem003(primeNumberProvider: IPrimeNumberProvider) =

    let maxPrime number = sqrt (float number) |> System.Math.Ceiling |> int64

    let factor number =
        let sieve = primeNumberProvider.Primes |> Seq.map int64 |> List.ofSeq
        let rec reduce sieve number = 
            match sieve with
                | head::tail when (number % head) = 0L -> head :: (reduce sieve (number / head))
                | head::tail when head > (maxPrime number) -> [number]
                | head::tail -> reduce tail number
                | [] -> [number]
        reduce sieve number |> List.filter ((<) 1L)
 
    let result () = factor 1000L |> List.max |> int

    interface IProblemSolution with
        member x.ProblemId = 3
        member x.SolutionAlgorithm with get () = result
        