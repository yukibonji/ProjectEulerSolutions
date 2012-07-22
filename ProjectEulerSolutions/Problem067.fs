namespace Iit.Fsharp.ProjectEulerSolutions
open System.Linq
open System.IO
open System.Reflection
open Iit.Fsharp.Toolkit.Core

type Tree<'a> =
    | Root of 'a
    | Node of 'a * Tree<'a> * Tree<'a>

type Problem067() =
    let toNumberList (line : string) = 
        line.Split([|' '|], System.StringSplitOptions.RemoveEmptyEntries)
        |> Array.map int
    let toJaggedList (string : string) =
        string.Split('\n')
        |> Array.map toNumberList
    let jaggedPyramid =
        use stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("triangle.txt")
        use reader = new StreamReader(stream)
        reader.ReadToEnd() |> toJaggedList
    
    let tree =
        let createLargerTree (subtrees : Tree<_>[]) (head : _[]) index =
            Node(head.[index], subtrees.[index], subtrees.[index + 1])
        let rec tree (jaggedPyramid : _[][]) (subtrees : Tree<_>[]) = 
            match Array.length jaggedPyramid with
                | 0 -> subtrees.Single()
                | length -> 
                    [| 0 .. length-1 |]
                    |> Array.map (createLargerTree subtrees jaggedPyramid.[0])
                    |> (tree jaggedPyramid.[1..])
        let revPyramid = Array.rev jaggedPyramid
        let (bottom, top) = (Array.map Root revPyramid.[0], revPyramid.[1..])
        tree top bottom
            
    let rec maxTreePath = cache HashIdentity.Reference (fun tree ->
        match tree with
        | Root(x) -> x
        | Node(x, leftTree, rightTree) ->
            x + (max (maxTreePath leftTree) (maxTreePath rightTree)))

    let result () = bigint (maxTreePath tree)

    interface IProblemSolution with
        member x.ProblemId = 67
        member x.SolutionAlgorithm with get () = result