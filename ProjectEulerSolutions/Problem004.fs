namespace Iit.Fsharp.ProjectEulerSolutions
open System.Linq

type Problem004() =
    let isSymmetric sequence =
        let orig = List.ofSeq sequence
        let reversed = List.rev orig
        orig = reversed
        
    let isPalindrome number = List.ofSeq (number.ToString()) |> isSymmetric

    let largestPalindrome =
        let left = [999..-1..100]
        let right = [999..-1..100]
        let findLargestPalindrome n = List.tryFind isPalindrome (List.map (fun x -> n * x) right)
        List.map findLargestPalindrome left |> List.max |> Option.get

    let result () = bigint (largestPalindrome)

    interface IProblemSolution with
        member x.ProblemId = 4
        member x.SolutionAlgorithm with get () = result