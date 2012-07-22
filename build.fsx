// include Fake libs
#I @"packages/FAKE.1.64.6/tools"
#r "FakeLib.dll"
open Fake

let buildDir = @"./build/"
let tempObjFolders = !! @"./**/obj"
let appReferences = !! @"**/*.fsproj"

Target "Clean" (fun _ ->
    CleanDir buildDir
    DeleteDirs tempObjFolders
)

Target "BuildApp" (fun _ ->
    MSBuildDebug buildDir "Build" appReferences
       |> Log "AppBuild-Output: "
    [] |> ignore
)

Target "Default" (fun _ ->
    trace "Hello World from FAKE"
)

"Clean"
    ==> "BuildApp"
    ==> "Default"

Run "Default"