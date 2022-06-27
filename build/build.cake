// #tool nuget:?package=xunit.runner.console&version=2.4.1
#load  "core.cake"



//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var _target = Argument("target", "Default");

// no defaults set to prevent accidental deployments
var _deploymentEnviroment = Argument("DeploymentEnvironment","");
var _publishSettings = Argument<string>("PublishSettings","");
var _configuration = Argument("configuration", "Release");
var _webSolution = "../src/ResearchTHM.API.sln";

var _webProj = "../src/ResearchTHM.API/ResearchTHM.API.csproj";

var _allSolutions = new []{  _webSolution };

// if you update these directories, you will need update FolderProfile.pubxml
var _publishFolderWeb = "../artifacts/";


var _websiteFolderWeb = "../src/ResearchTHM.API";


var _xunitTestResultsDirectory = $"./artifacts/UnitTestResults";

var consoleColor =  Console.ForegroundColor;
Console.ForegroundColor = ConsoleColor.White;

System.Console.WriteLine(@"

   _____ _                              __  __      _
  / ____| |                            |  \/  |    | |
 | |    | |__   __ _ _ __   __ _  ___  | \  / | ___| |
 | |    | '_ \ / _` | '_ \ / _` |/ _ \ | |\/| |/ _ \ |
 | |____| | | | (_| | | | | (_| |  __/ | |  | |  __/_|
  \_____|_| |_|\__,_|_| |_|\__, |\___| |_|  |_|\___(_)
                            __/ |
                           |___/

    ");

Console.ForegroundColor = consoleColor;

// commented out - not showing up in BlueModus.Kentico.Scripts yet
DisplayBuildInformation();


//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////


Task("Clean")
.Does(() =>
    {
    // delete Precompiled web directory - otherwise deleted/ renamed files and directories
    // remain in the directory.
    // CleanDirectory(System.IO.Path.Combine(_publishFolderWeb,"/*"));
    CleanDirectory(_xunitTestResultsDirectory);
    CleanSolutions(_allSolutions);

    // remove EVERYTHING in the .\server\CMS\bin directory.
    // some files get XCOPYd in here, so using MSBUILD clean will not remove
    // files it does not explicity know about e.g. ContinuousIntegration.exe
    CleanDirectory(System.IO.Path.Combine(_websiteFolderWeb,"bin"));

    });

Task("Restore-NuGet-Packages")
.Does(() =>
{
    RestoreNuGetPackages();
});


Task("Build")
.Does(() =>
{
    DisplayBuildInformation();
    BuildSolution(_webSolution);
});



Task("UnitTests")
.Does(() =>
{   
    var projects = GetFiles("./../src/*Tests/*.csproj");
    if (projects.Count==0)
    {
        Information("No test projects found to run unit tests.");
        return;
    }
    foreach(var project in projects)
    {
        DotNetTest(
            project.FullPath,
            new DotNetCoreTestSettings()
            {
                Configuration = _configuration,
                NoBuild = false,             
                VSTestReportPath= $"./../artifacts/UnitTestResults/{project.GetFilename()}.trx",
            });
        }
});



Task("PublishWeb")
.Description("Publishes to PrecompiledWeb directory")
.Does(()=>
{
    PublishProject(_webProj);

    // CopyExtraWebAssets( _websiteFolderWeb, _publishFolderWeb+"/src/");
    CopyCakeAndEtcDirectory(_websiteFolderWeb, _publishFolderWeb);


});


Task("DeployWeb")
.Description("Deploys API with WebDeploy")
.Does(()=>
{

    var directoryToDeploy ="../src/PrecompiledWeb";
    if (!DirectoryExists(directoryToDeploy))
    {
        Information("Running on DEV machine.");
        // when running from a DEV machine
        directoryToDeploy = _publishFolderWeb+"/src/PrecompiledWeb";

    }
    Deploy(_deploymentEnviroment, directoryToDeploy);
});


//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////



Task("CIBuild")
.IsDependentOn("Clean")
.IsDependentOn("Restore-NuGet-Packages")
.IsDependentOn("Build")
.IsDependentOn("UnitTests")
.IsDependentOn("PublishWeb");

Task("Default")
// .IsDependentOn("NpmInstall")
.IsDependentOn("Restore-NuGet-Packages")
.IsDependentOn("Build")
.IsDependentOn("PublishWeb")
.IsDependentOn("UnitTests");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(_target);
