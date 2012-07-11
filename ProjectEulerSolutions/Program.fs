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

let interestingProblemIds = [218]

let results = new Dictionary<int, unit -> bigint>()

kernel.GetAll<IProblemSolution>()
|> Seq.iter (fun solution -> results.Add (solution.ProblemId, solution.SolutionAlgorithm))

System.Console.OutputEncoding <- System.Text.Encoding.UTF8

let execute problemId =
    let value = (results.[problemId]()).ToString()
    printfn "Problem #%d has the solution %s" problemId value

interestingProblemIds
    |> Seq.filter results.ContainsKey
    |> Seq.iter execute

let rnd = new System.Random()
let array = Set.difference (set {1..391}) (set results.Keys) |> Set.toArray 

printfn "now you ought to solve the problem #%d... may the lambda be with you..." array.[rnd.Next array.Length]
System.Console.ReadKey() |> ignore