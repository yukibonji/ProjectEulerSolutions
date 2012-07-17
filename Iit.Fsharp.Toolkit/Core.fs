namespace Iit.Fsharp.Toolkit

open System.Collections.Generic
open System.Linq
module Core =
    let cache (hashIdentity : IEqualityComparer<_>) f =
        let valueCache = new Dictionary<_,_>(hashIdentity)
        fun x ->
            let ok, result = valueCache.TryGetValue(x)
            if ok then
                result
            else
                let result = f x
                valueCache.[x] <- result
                result

    let inline gcd x y =
        let rec gcd x y = 
            if y = LanguagePrimitives.GenericZero then x
            else gcd y (x % y)
        gcd x y