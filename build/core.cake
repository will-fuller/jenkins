// If you think you need to edit this file for some reason... STOP!!! Ask the Devops team first. It is likely you are doing the wrong thing!

using System.Net;

public void DisplayBuildInformation()
{
	Information($"DeploymentEnvironment:{_deploymentEnviroment}");
	Information($"Configuration:{_configuration}");
}



public void  MSDeployUsingPublishSettingsFileChangesOnly(string publishFolder, string publishSettingsFile, string folder)
{
	if (!FileExists(publishSettingsFile))
	{
		throw new FileNotFoundException("Could not find settings file:"+publishSettingsFile);
	}

	publishFolder=publishFolder+folder;

 	Information("Syncing folder:"+folder);

	var publishUrl = XmlPeek(publishSettingsFile, "/publishData/publishProfile[@publishMethod='MSDeploy']/@publishUrl");
	var username = XmlPeek(publishSettingsFile, "/publishData/publishProfile[@publishMethod='MSDeploy']/@userName");
	var password = XmlPeek(publishSettingsFile, "/publishData/publishProfile[@publishMethod='MSDeploy']/@userPWD");
	var siteName = XmlPeek(publishSettingsFile, "/publishData/publishProfile[@publishMethod='MSDeploy']/@msdeploySite");

	publishUrl = "https://"+publishUrl+"/msdeploy.axd";
	Information("Publish URL:"+publishUrl);
	Information("SiteName:"+siteName);
	Information("Username:"+username);


	var msDeploy="C:/Program Files (x86)/IIS/Microsoft Web Deploy V3/msdeploy.exe";
	var arguments = new List<string>()
	{
		"-verb:sync",
		"-allowUntrusted:true",
		"-source:contentPath="+publishFolder,
		"-dest:contentPath="+siteName + folder +",wmsvc="+publishUrl+",password="+password+	",username="+username,
		"-enableRule:AppOffline",
		"-useCheckSum"
	};

	var process = StartAndReturnProcess(msDeploy,new ProcessSettings
	{
		Arguments = string.Join(" ",arguments)
	});

	process.WaitForExit();
	var exitCode = process.GetExitCode();
	if (exitCode != 0)
	{
		throw new Exception(String.Format("MSdeploy failed. (Exit code was {0}().",exitCode));
	}
}

public void MSDeployUsingPublishSettingsFile(string publishFolder, string publishSettingsFile, bool addDoNotDeleteRule=true)
{
	if (!FileExists(publishSettingsFile))
	{
		throw new FileNotFoundException("Could not find settings file:"+publishSettingsFile);
	}

	Information("PublishSettingsFile:"+publishSettingsFile);

	var publishUrl = XmlPeek(publishSettingsFile, "/publishData/publishProfile[@publishMethod='MSDeploy']/@publishUrl");
	var username = XmlPeek(publishSettingsFile, "/publishData/publishProfile[@publishMethod='MSDeploy']/@userName");
	var password = XmlPeek(publishSettingsFile, "/publishData/publishProfile[@publishMethod='MSDeploy']/@userPWD");
	var siteName = XmlPeek(publishSettingsFile, "/publishData/publishProfile[@publishMethod='MSDeploy']/@msdeploySite");

	publishUrl = "https://"+publishUrl+"/msdeploy.axd";
	Information("Publish URL:"+publishUrl);
	Information("SiteName:"+siteName);
	Information("Username:"+username);

	// recommended directories to skip

	var directoriesToSkip=new string[]
	{
		@"App_Data\AzureCache",

	};

	var filesToSkip= new string[]
	{
		@"App_Data\CMSModules\DeviceProfiles\logFiftyOne.txt"
	};

	var directoriesToSkipParams =directoriesToSkip.Select(dir=>string.Format("-skip:Directory=\"{0}\"",dir.Replace("\\","\\\\")));
	var filesToSkipParams =filesToSkip.Select(dir=>string.Format("-skip:File=\"{0}\"",dir.Replace("\\","\\\\")));
	var skipParams  = directoriesToSkipParams.Concat(filesToSkipParams).ToList();



	Information("Skipping the following:");
	skipParams.ToList().ForEach(p=> Information(string.Format("  {0} {1}",skipParams.IndexOf(p)+1,p)));

	var msDeploy="C:/Program Files (x86)/IIS/Microsoft Web Deploy V3/msdeploy.exe";
	var arguments = new List<string>()
	{
		"-verb:sync",
		"-allowUntrusted:true",
		"-source:contentPath="+publishFolder,
		"-dest:contentPath="+siteName+ ",wmsvc="+publishUrl+",password="+password+	",username="+username,
		"-enableRule:AppOffline",
		"-useChecksum",
		String.Join(" ", skipParams)
	};

	if (addDoNotDeleteRule)
	{
		arguments.Add("-enableRule:DoNotDeleteRule");
	}

	var process = StartAndReturnProcess(msDeploy,new ProcessSettings
	{
		Arguments = string.Join(" ",arguments)
	});

	process.WaitForExit();
	var exitCode = process.GetExitCode();
	if (exitCode != 0)
	{
		throw new Exception(String.Format("MSdeploy failed. (Exit code was {0}().",exitCode));
	}
}



public void CopyExtraWebAssets(string sourceFolder, string publishFolder)
{
	Information($"CopyExtraWebAssets");
	Information($"	Source: {sourceFolder}");
	Information($"	Destination: {publishFolder}");

	Information("Copy Dist folder");
	CopyDirectoryIfExists(sourceFolder+"/dist", publishFolder + "/dist");

	Information("Copy Img folder");
	CopyDirectoryIfExists(sourceFolder+"/img", publishFolder + "/img");
}



public void CopyCakeAndEtcDirectory(string sourceFolder, string publishFolder )
{
	Information($"CopyCakeAndEtcDirectory");
	Information($"	Source: {sourceFolder}");
	Information($"	Destination: {publishFolder}");


	Information($"Copy PS1 files to {publishFolder}");
	CopyFiles("../*.ps1", publishFolder);

	Information($"Copy CAKE files {publishFolder}");
	CopyDirectory("../build", publishFolder+"/build");

	// so that when we deploy, the Cake dotnet tool is available
	Information($"Copy .config {publishFolder}");
	CopyDirectory("../.config", publishFolder+"/.config");
}

public void CopyConfigs(string sourceFolder, string destFolder)
{
	Information($"Copy Config Files");
	Information($"	Source:{sourceFolder}");
	Information($"	Dest:{destFolder}");
	CreateDirectory(destFolder);
	CopyFiles(sourceFolder + "/web*.config",destFolder);
}

public void PublishProject(string projectFile)
{
	Information($"Building {projectFile}");

	// make TMP be shorter so we can handle longer paths
	// by default TMP is %USERPROFILE%\AppData\Local\Temp
	// this causes file paths to exceed 260 characters which
	// causes the publish step to break.
	var tmp = Environment.GetEnvironmentVariable("TMP");
	Environment.SetEnvironmentVariable("TMP",@"C:\TEMP");

	MSBuild(projectFile, settings => settings
		.UseToolVersion(MSBuildToolVersion.VS2022)
		.SetConfiguration(_configuration)
		.SetMaxCpuCount(0)
		.SetVerbosity(Verbosity.Minimal)
		.WithProperty("PublishProfile","FolderProfile")
		.WithProperty("DeployOnBuild","true")
		.WithProperty("AllowUntrustedCertificate", "true"));

	// reset TMP environment variable back to what it was
	Environment.SetEnvironmentVariable("TMP",tmp);
}

public void BuildSolution(string projectFile)
{
	Information($"Building {projectFile}");

	MSBuild(projectFile, settings => settings.SetConfiguration(_configuration)
		.UseToolVersion(MSBuildToolVersion.VS2022)
		.SetMaxCpuCount(0)
		.SetVerbosity(Verbosity.Minimal));
}

public void CleanDirectory(string directory)
{
	if (DirectoryExists(directory))
	{
		Information($"Removing {directory}");
		DeleteDirectory(directory, new DeleteDirectorySettings { Recursive = true, Force = true});
	}
}

public void CleanSolutions(string[] allSolutions)
{
	foreach(var sln in allSolutions)
    {
        MSBuild(sln, settings => settings.SetConfiguration(_configuration)
	  	.SetVerbosity(Verbosity.Minimal)
		.WithTarget("Clean"));
    }
}


public void RestoreNuGetPackages()
{
	var srcDirectoryPath = MakeAbsolute(Directory("./../src")).FullPath;
	var packagesDir = System.IO.Path.Combine(srcDirectoryPath, "packages");

	var nugetPath = MakeAbsolute(new FilePath("./../src/nuget.config"));
	Information("Nuget.config:"+nugetPath);

	var srcDirectoryInfo = new System.IO.DirectoryInfo(srcDirectoryPath);
	foreach(var csProjFileInfo in srcDirectoryInfo.EnumerateFiles("*.csproj", System.IO.SearchOption.AllDirectories))
	{
		var dir = csProjFileInfo.Directory.FullName;
		var packagesPath = System.IO.Path.Combine(dir,"packages.config");
		var packagesConfigExists = System.IO.File.Exists(packagesPath);

		if (packagesConfigExists)
		{
			// if a packages.config exists - then is the 'old' style requires NuGet to be used to restore packages
			Information($"{csProjFileInfo.Name} (NuGet restore)");
			var toolPath = MakeAbsolute(Directory("./../tools/nuget.exe")).FullPath;
			Information($"Toolpath: {toolPath}");
			NuGetRestore(csProjFileInfo.FullName, new NuGetRestoreSettings
			{
				PackagesDirectory = packagesDir,
				ToolPath =toolPath,
				ConfigFile = nugetPath
			} );
		}	
		else
		{
			// new style .csproj that contains references to NuGet packages. use `dotnet restore`
			// we need to add the NuGet sources here - since the build agent might not neccessariy have
			// them defined.
			Information($"{csProjFileInfo.Name} (DotNet restore)");
			var restoreSettings = new DotNetRestoreSettings()
			{
				PackagesDirectory = packagesDir,
				ConfigFile = nugetPath
			};
			DotNetRestore(csProjFileInfo.FullName, restoreSettings );
		}	
	}
}

public void Deploy(string deploymentEnviroment, string precompiledWebFolder)
{

	if(deploymentEnviroment == null || deploymentEnviroment == "")
	{
		throw new ArgumentException("deploymentEnviroment must have a value.");
	}

	Information("Deploying to "+ deploymentEnviroment.ToUpper());
	Information($"	Environment:{deploymentEnviroment}");
	Information($"	ProcompiledWebFolder:{precompiledWebFolder}");


	var publishDir = MakeAbsolute(new DirectoryPath(precompiledWebFolder));

	var doNotDeleteRule=false;
	Information($"DoNotDelete Rule is {doNotDeleteRule}.");


	MSDeployUsingPublishSettingsFile(publishDir.FullPath, _publishSettings, doNotDeleteRule);

}

Action<string,string> CopyDirectoryIfExists = (source, destination)=>{
	Information($"CopyDirectoryIfExists");
	Information($"	Source:{source}");
	Information($" 	Destination:{destination}");

	if(DirectoryExists(source))
	{
		Information($"	Source directory exists");
		CopyDirectory(source, destination);
	}
};

