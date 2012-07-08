namespace Iit.Fsharp.ProjectEulerSolutions

type IPrimeNumberProvider =
    abstract Primes : unit -> int seq with get 