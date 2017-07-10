// include Fake lib
#r "../../packages/build/FAKE/tools/FakeLib.dll"
open Fake

// Properties
let buildDir = "./build/output"

// Targets
Target "Clean" <| fun _ -> CleanDir buildDir


Target "BuildApp" <| fun _ ->
    !! "./**/*.csproj"
    |> MSBuildRelease buildDir "Build"
    |> Log "AppBuild-Output: "

Target "Build" <| fun _ -> trace "Build Nest.Geospatial"

// Dependencies
"Clean"
  ==> "BuildApp"
  ==> "Build"

// start build
RunTargetOrDefault "Build"