namespace Iit.Fsharp.ProjectEulerSolutions
open System.Linq

type Problem007(primeNumberProvider: IPrimeNumberProvider) =
    let result () = bigint (primeNumberProvider.Primes |> Seq.nth 10000)

    interface IProblemSolution with
        member x.ProblemId = 7
        member x.SolutionAlgorithm with get () = result