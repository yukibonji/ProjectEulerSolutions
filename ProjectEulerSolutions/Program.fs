open Ninject
open Ninject.Extensions.Conventions
open System.Linq
open Iit.Fsharp.ProjectEulerSolutions
open System.Collections.Generic

let kernel = new StandardKernel();;
kernel.Bind(fun (x: Syntax.IFromSyntax) ->
    x.FromThisAssembly()
     .SelectAllClasses()
     .BindAllInterfaces() |> ignore)

let interestingProblemIds = [1]

let results = new Dictionary<int, unit -> int>()

let solutions = kernel.GetAll<IProblemSolution>()
                |> Seq.iter (fun solution -> results.Add (solution.ProblemId, solution.SolutionAlgorithm))

interestingProblemIds
    |> Seq.iter (fun problemId -> printfn "Problem #%d has the solution %d" problemId (results.[problemId]()))