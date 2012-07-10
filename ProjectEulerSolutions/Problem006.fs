namespace Iit.Fsharp.ProjectEulerSolutions
open System.Linq

type Problem006() =
    let square x = x * x
    let sumOfSquares list = list |> List.map square |> List.sum
    let squareOfSum list = list |> List.sum |> square

    let result () = squareOfSum [1I..100I] - sumOfSquares [1I..100I]

    interface IProblemSolution with
        member x.ProblemId = 6
        member x.SolutionAlgorithm with get () = result