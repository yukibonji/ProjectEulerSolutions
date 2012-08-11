// include Fake libs
#I @"packages/FAKE.1.64.6/tools"
#r "FakeLib.dll"
open Fake

let buildDir = @"./build/"
let tempObjFolders = !+ @"./**/obj" ++ @"./**/bin" |> Scan
let appReferences = !! @"**/*.fsproj"

Target "Clean" (fun _ ->
    CleanDir buildDir
)

Target "Default" (fun _ ->
    MSBuildDebug buildDir "Build" appReferences
       |> Log "AppBuild-Output: "
    [] |> ignore
)

Target "DeleteTempDirs" (fun _ ->
    DeleteDirs tempObjFolders
)

"Clean"
    ==> "Default"
    ==> "DeleteTempDirs"

Run "DeleteTempDirs"