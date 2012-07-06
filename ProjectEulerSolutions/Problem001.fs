namespace Iit.Fsharp.ProjectEulerSolutions

type Problem001() =
    let result () = 2
    interface IProblemSolution with
        member x.ProblemId = 1
        member x.SolutionAlgorithm with get () = result