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
                let result = f x
                valueCache.[key] <- result
                result

    let rec gcd x y =
        if y = 0L then x
        else gcd y (x % y)