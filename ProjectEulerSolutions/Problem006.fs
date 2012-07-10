namespace Iit.Fsharp.ProjectEulerSolutions
open System.Linq

type Problem006() =
    let square (x: int) = x * x
    let sumOfSquares list = list |> List.map square |> List.sum
    let squareOfSum list = list |> List.sum |> square

    let result () = squareOfSum [1..100] - sumOfSquares [1..100]

    interface IProblemSolution with
        member x.ProblemId = 6
        member x.SolutionAlgorithm with get () = result