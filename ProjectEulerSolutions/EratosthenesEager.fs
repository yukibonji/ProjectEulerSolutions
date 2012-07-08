namespace Iit.Fsharp.ProjectEulerSolutions

type EratosthenesEager(number: int64) =
    let rec sieve list = 
        let relPrime divisor dividend = not ((dividend % divisor) = 0)
        match list with
            | head::tail -> head :: (sieve (tail |> List.filter (relPrime head)))
            | [] -> []

    let maxPrime number = sqrt (float number) |> System.Math.Ceiling |> int

    let sieveFor number =
        sieve [2..(maxPrime number)]

    interface IPrimeNumberProvider with
        member x.Primes with get () = seq (sieveFor number)