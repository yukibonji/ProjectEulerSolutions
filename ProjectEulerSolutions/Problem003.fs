namespace Iit.Fsharp.ProjectEulerSolutions
open System.Linq

type Problem003() =

    let rec sieve list = 
        let relPrime divisor dividend = not ((dividend % divisor) = 0L)
        match list with
            | head::tail -> head :: (sieve (tail |> List.filter (relPrime head)))
            | [] -> []

    let maxPrime (number: int64) = sqrt (float number) |> System.Math.Ceiling |> int64

    let sieveFor number =
        sieve [2L..(maxPrime number)]
    let factor number =
        let sieve = sieveFor number
        let rec reduce sieve number = 
            match sieve with
                | head::tail when (number % head) = 0L -> head :: (reduce sieve (number / head))
                | head::tail when head > (maxPrime number) -> [number]
                | head::tail -> reduce tail number
                | [] -> [number]
        reduce sieve number |> List.filter ((<) 1L)

    let result () = factor 600851475143L |> List.max |> int

    interface IProblemSolution with
        member x.ProblemId = 3
        member x.SolutionAlgorithm with get () = result