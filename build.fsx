// include Fake libs
#I @"lib/FAKE-1.64.6.0"
#r "FakeLib.dll"
open Fake

let buildDir = @"./build/"

/// Delete all non-source files to have a clean build environment.
Target "Clean" (fun _ ->
    CleanDir buildDir
)

/// The default build target.
Target "Default" (fun _ ->
    trace "Hello World from FAKE"
)

"Clean"
    ==> "Default"

Run "Default"