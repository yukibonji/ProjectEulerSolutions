// include Fake libs
#I @"lib/FAKE-1.64.6.0"
#r "FakeLib.dll"
open Fake 

Target "Default" (fun _ ->
    trace "Hello World from FAKE"
)

Run "Default"