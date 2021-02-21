#load nuget:?package=Cake.Recipe&version=2.2.0

Environment.SetVariableNames();

BuildParameters.SetParameters(
    context: Context,
    buildSystem: BuildSystem,
    sourceDirectoryPath: "./src",
    title: "Cake.Figlet",
    repositoryOwner: "cake-contrib",
    repositoryName: "Cake.Figlet",
    appVeyorAccountName: "cakecontrib",
    preferredBuildProviderType: BuildProviderType.GitHubActions,
    shouldRunDotNetCorePack: true,
    shouldGenerateDocumentation: false);

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(
    context: Context,
    dupFinderExcludePattern: new string[] { BuildParameters.RootDirectoryPath + "/src/Cake.Figlet.Tests/*.cs", BuildParameters.RootDirectoryPath + "/src/Cake.Figlet*/**/*.AssemblyInfo.cs" },
    testCoverageFilter: "+[*]* -[xunit.*]* -[Cake.Core]* -[Cake.Testing]* -[*.Tests]* -[Shouldly]*",
    testCoverageExcludeByAttribute: "*.ExcludeFromCodeCoverage*",
    testCoverageExcludeByFile: "*/*Designer.cs;*/*.g.cs;*/*.g.i.cs");

Build.RunDotNetCore();
