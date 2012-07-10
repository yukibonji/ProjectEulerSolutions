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
     
kernel.Bind<IPrimeNumberProvider>()
      .To<EratosthenesEager>()
      .WhenInjectedInto<Problem003>()
      .WithConstructorArgument("number", 600851475143L)
      |> ignore

let interestingProblemIds = [1..391]

let results = new Dictionary<int, unit -> bigint>()

kernel.GetAll<IProblemSolution>()
|> Seq.iter (fun solution -> results.Add (solution.ProblemId, solution.SolutionAlgorithm))

let execute problemId =
    let value = (results.[problemId]()).ToString()
    printfn "Problem #%d has the solution %s" problemId value

interestingProblemIds
    |> Seq.filter results.ContainsKey
    |> Seq.iter execute

System.Console.ReadKey() |> ignore