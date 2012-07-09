namespace Iit.Fsharp.ProjectEulerSolutions

type EratosthenesEager(number: int64) =

    let sieve maxPrime = 
        let (list : bool[]) = Array.create (maxPrime + 1) true
        let markOff number = [2*number..number..list.Length] |> List.iter (fun index -> list.[index] <- false)
        let rec sieveNext number =
            if number >= list.Length then
                ()
            if list.[number] = true then
                markOff number
            sieveNext (number + 1)
        sieveNext 2
        list

    let maxPrime number = sqrt (float number) |> System.Math.Ceiling |> int

    let sieveFor number =
        sieve (maxPrime number)

    interface IPrimeNumberProvider with
        member x.Primes with get () = seq (sieveFor number)