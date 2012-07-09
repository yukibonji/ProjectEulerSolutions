namespace Iit.Fsharp.ProjectEulerSolutions

type EratosthenesEager(number: int64) =

    let sieve maxPrime = 
        let (list : bool[]) = Array.create (maxPrime + 1) true
        list.[0] <- false
        list.[1] <- false
        let markOff number = [2*number..number..list.Length - 1] |> List.iter (fun index -> list.[index] <- false)
        let rec sieveNext number =
            if number >= list.Length then
                ()
            else
                markOff number
                sieveNext (number + 1)
        sieveNext 2
        list
        
    let maxPrime number = sqrt (float number) |> System.Math.Ceiling |> int

    let sieveFor number =
        sieve (maxPrime number)

    let indexList selector sequence = 
        let selectIndex index item =
            if selector item then
                Some(index)
            else
                None 
        let indexes = Seq.mapi selectIndex sequence
        Seq.choose id indexes

    interface IPrimeNumberProvider with
        member x.Primes with get () = indexList id (sieveFor number)