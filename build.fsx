// include Fake libs
#I @"lib/FAKE-1.64.6.0"
#r "FakeLib.dll"
open Fake

let buildDir = @"./build/"
let appReferences = !! @"**\*.fsproj"

Target "Clean" (fun _ ->
    CleanDir buildDir
)

Target "BuildApp" (fun _ ->
    MSBuildRelease buildDir "Build" appReferences
        |> Log "AppBuild-Output: "
)

Target "Default" (fun _ ->
    trace "Hello World from FAKE"
)

"Clean"
    ==> "BuildApp"
    ==> "Default"

Run "Default"