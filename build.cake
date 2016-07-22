#tool "nuget:?package=GitVersion.CommandLine"
#tool "xunit.runner.console"

var target = Argument<string>("target", "build");
var configuration = Argument<string>("configuration", "Release");

// Get whether or not this is a local build.
var local = BuildSystem.IsLocalBuild;
var isRunningOnAppVeyor = AppVeyor.IsRunningOnAppVeyor;
var isPullRequest = AppVeyor.Environment.PullRequest.IsPullRequest;

var nuspec = File("./Cake.Figlet.nuspec");
var solutionPath = File("./Cake.Figlet.sln");
var artifacts = "./build/";
var testResultsPath = MakeAbsolute(Directory(artifacts + "./test-results"));

var nugetVersion = "";
var semVer = "";

Task("clean")
    .Does(() => 
{
    CleanDirectories("./build");
    CleanDirectories("./**/bin");
    CleanDirectories("./**/obj");
    CreateDirectory("./build");
});

Task("set-version")
    .Does(() => 
{
    var version = GitVersion(new GitVersionSettings {
        UpdateAssemblyInfo = true
    });

    semVer = version.SemVer;
    nugetVersion = version.NuGetVersion;
    Information("Nuget Version: " + nugetVersion);
    Information("SemVer: " + semVer);
});

Task("build")
    .IsDependentOn("set-version")
    .IsDependentOn("clean")
.Does(() => 
{
    NuGetRestore(solutionPath);
    DotNetBuild(solutionPath, settings =>
        settings.SetConfiguration("Debug")
            .SetVerbosity(Verbosity.Minimal)
            .SetConfiguration("release")
            .WithProperty("TreatWarningsAsErrors","true"));
});

Task("test")
    .IsDependentOn("build")
    .Does(() => 
{
    XUnit2("tests/Cake.Figlet.Tests/bin/release/Cake.Figlet.Tests.dll",
        new XUnit2Settings {
            Parallelism = ParallelismOption.All,
            XmlReport = true,
            OutputDirectory = "./build"
        });

    if(AppVeyor.IsRunningOnAppVeyor)
    {
        Information("Uploading test results");
        AppVeyor.UploadTestResults("./build/Cake.Figlet.Tests.dll.xml", AppVeyorTestResultsType.XUnit);
    }
});

Task("package")
    .IsDependentOn("build")
    .Does(() => 
{
    CreateDirectory("./nupkg/");

    NuGetPack(nuspec, new NuGetPackSettings {
        OutputDirectory = "./nupkg/",
        Version = nugetVersion
    });	
});

Task("push-to-nuget")
    .IsDependentOn("package")
    .WithCriteria(() => isRunningOnAppVeyor)
    .WithCriteria(() => !isPullRequest)
    .Does(() =>
{
    // Get the newest(by last write time) to publish
    var newestNupkg = GetFiles("nupkg/*.nupkg")
        .OrderBy(f => new System.IO.FileInfo(f.FullPath).LastWriteTimeUtc)
        .LastOrDefault();

    var apiKey = EnvironmentVariable("NUGET_API_KEY");

    NuGetPush(newestNupkg, new NuGetPushSettings { 
        Verbosity = NuGetVerbosity.Detailed,
        Source = "https://www.nuget.org/api/v2/package",
        ApiKey = apiKey
    });
});

Task("update-appveyor-build-number")
    .WithCriteria(() => isRunningOnAppVeyor)
    .Does(() =>
{
    AppVeyor.UpdateBuildVersion(semVer);
});

Task("appveyor")
    .IsDependentOn("push-to-nuget")
    .IsDependentOn("update-appveyor-build-number");

Task("default")
    .IsDependentOn("package");

RunTarget(target);
