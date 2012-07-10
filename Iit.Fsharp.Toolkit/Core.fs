namespace Iit.Fsharp.Toolkit

open System.Collections.Generic
open System.Linq
module Core =

    let cache f =
        let valueCache = new Dictionary<_,_>()
        fun x ->
            let ok, result = valueCache.TryGetValue(x)
            if ok then
                result
            else
                //printf "%A has " x
                let result = f x
                valueCache.[x] <- result
                //printfn "%d elements" (Set valueCache.[x]).Count
                result