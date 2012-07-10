namespace Iit.Fsharp.Toolkit

open System.Collections.Generic
open System.Linq
module Core =

    let cache f hashSelector =
        let valueCache = new Dictionary<_,_>()
        fun x ->
            let key = hashSelector x
            let ok, result = valueCache.TryGetValue(key)
            if ok then
                result
            else
//              printf "%A has " key
                let result = f x
                valueCache.[key] <- result
//              printfn "%d elements" (Set valueCache.[key]).Count
                result