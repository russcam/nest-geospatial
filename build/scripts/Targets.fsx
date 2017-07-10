// include Fake lib
#r "../../packages/build/FAKE/tools/FakeLib.dll"
open Fake
open Fake.Testing.XUnit2

// Properties
let buildDir = "./build"
let buildOutputDir = buildDir @@ "/output"

// Targets
Target "Clean" <| fun _ -> CleanDir buildOutputDir

Target "BuildApp" <| fun _ ->
    !! "./**/*.csproj"
    |> MSBuildRelease buildOutputDir "Build"
    |> Log "AppBuild-Output: "

Target "Build" <| fun _ -> trace "Build Nest.Geospatial"

Target "Test" <| fun _ ->
    !! (buildOutputDir @@ "Nest.GeoSpatial.Tests.dll")
    |> xUnit2 (fun p -> { p with HtmlOutputPath = Some (buildDir @@ "xunit.html") }) 

// Dependencies
"Clean"
  ==> "BuildApp"
  ==> "Build"

"BuildApp"
  ==> "Test"

// start build
RunTargetOrDefault "Build"