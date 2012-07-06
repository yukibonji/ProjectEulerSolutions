namespace Iit.Fsharp.ProjectEulerSolutions

type IProblemSolution =
    abstract ProblemId : int
    abstract SolutionAlgorithm : unit -> (unit -> int) with get 