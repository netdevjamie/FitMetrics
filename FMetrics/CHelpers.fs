// methods that will appear as extension methods to C#
[<System.Runtime.CompilerServices.Extension>]
module FMetrics.CHelpers

open System.Runtime.CompilerServices

open FMetrics.BReusable

 [<Extension>]
let Before(this:string,delimiter:string) : string =
    this |> String.before delimiter
