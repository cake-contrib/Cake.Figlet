#tool "nuget:?package=GitVersion.CommandLine"
#tool "xunit.runner.console"


var target = Argument<string>("target", "build");
var configuration = Argument<string>("configuration", "Release");
var projectName = "Cake.Figlet";

// Get whether or not this is a local build.
var local = BuildSystem.IsLocalBuild;
var isRunningOnAppVeyor = AppVeyor.IsRunningOnAppVeyor;
var isPullRequest = AppVeyor.Environment.PullRequest.IsPullRequest;

var solutionPath = File("./Cake.Figlet.sln");
var artifacts = "./build/";
var testResultsPath = MakeAbsolute(Directory(artifacts + "./test-results"));
var sharedSolutionInformation = File("./Directory.Build.props");

var nugetVersion = "";
var semVer = "";

Setup(context => {
//    Information(Figlet("Cake.Figlet"));
});

Task("clean")
    .Does(() =>
{
    CleanDirectories("./build");
    CleanDirectories("./**/bin");
    CleanDirectories("./**/obj");
    CreateDirectory("./build");
});

Task("get-version")
    .Does(() =>
{
    var version = GitVersion(new GitVersionSettings());

    semVer = version.SemVer;
    nugetVersion = version.NuGetVersionV2;

    Information("SemVer: " + semVer);
    Information("NuGet: " + nugetVersion);
});

Task("set-version")
    .Does(() =>
{
	var version = GitVersion(new GitVersionSettings {
        UpdateAssemblyInfo = true
    });

	semVer = version.SemVer;
	nugetVersion = version.NuGetVersionV2;

    ReplaceProperty("Version", nugetVersion);
    ReplaceProperty("Product", projectName);
    ReplaceProperty("Description", projectName);
    ReplaceProperty("Company", "Cake.Figlet");
    ReplaceProperty("Copyright", "Copyright (c) " + DateTime.Now.Year);

	Information("Nuget Version: " + nugetVersion);
    Information("SemVer: " + semVer);
});

Task("__RestoreNugetPackages")
    .Does(() =>
{
    Information("Restoring packages for {0}", solutionPath);

    DotNetCoreRestore(solutionPath);
});

Task("build")
    .IsDependentOn("set-version")
    .IsDependentOn("clean")
	.IsDependentOn("__RestoreNugetPackages")
.Does(() =>
{
	DotNetCoreBuild(solutionPath, new DotNetCoreBuildSettings {
        Configuration = configuration,
    });
});

Task("test")
	.IsDependentOn("build")
    .Does(() =>
{
    var xunitArgs = "-Appveyor -nobuild -parallel none -configuration " + configuration;

	var project = "tests/Cake.Figlet.Tests/Cake.Figlet.Tests.csproj";

    Information("Testing project {0} with args {1}", project, xunitArgs);
    DotNetCoreTool(project, "xunit", xunitArgs);

	//if(AppVeyor.IsRunningOnAppVeyor)
    //{
    //    Information("Uploading test results");
    //    AppVeyor.UploadTestResults("./build/Cake.Figlet.Tests.dll.xml", AppVeyorTestResultsType.XUnit);
    //}
});

Task("package")
	.IsDependentOn("test")
    .Does(() =>
{
    CreateDirectory("./nupkg/");

	var project = "src/Cake.Figlet/Cake.Figlet.csproj";

	Information("Packaging library project {0}", project);

    DotNetCorePack(project, new DotNetCorePackSettings
    {
        Configuration = configuration,
        OutputDirectory = "./nupkg/",
        NoBuild = true
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

///////////////////////////////////////////////////////////////////////////////
// HELPERS
///////////////////////////////////////////////////////////////////////////////
void ReplaceProperty(string propertyName, string replacement) {
    Information("Setting {0} to {1}", propertyName, replacement);
    XmlPoke(sharedSolutionInformation, "/Project/PropertyGroup/" + propertyName, replacement);
}