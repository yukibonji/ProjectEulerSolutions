// include Fake libs
#r "FakeLib.dll"
open Fake 

Target "Default" (fun _ ->
    trace "Hello World from FAKE"
)

Run "Default"