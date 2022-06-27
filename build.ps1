[CmdletBinding()]
Param(

    # Targets/Action
    [ValidateSet("Test", "Build", "CIBuild", "UnitTests", "BuildFrontEnd", "Clean", "DeployWeb", "PublishWeb", "Restore-NuGet-Packages", "InstallBuildTools", "Sonar")]
    [string]$Target = "Default",

    # Configurations
    [ValidateSet("Release", "Debug","Development","Production", "Syven")]
    [string]$Configuration = "Development",

    # Environments
    [ValidateSet("DEV", "TST", "UAT", "STG", "PRD")]
    [string]$DeploymentEnvironment = "",

    [string]$PublishSettings
)

$ErrorActionPreference = 'Stop'

Set-Location -LiteralPath $PSScriptRoot



$env:DOTNET_SKIP_FIRST_TIME_EXPERIENCE = '1'
$env:DOTNET_CLI_TELEMETRY_OPTOUT = '1'
$env:DOTNET_NOLOGO = '1'



$cakeArgs = @()
if ($Target) {
    $cakeArgs = $cakeArgs + "--Target=$Target"
}
if ($Configuration) {
    $cakeArgs = $cakeArgs + "--Configuration=$Configuration"
}
if ($PublishSettings) {
    $cakeArgs = $cakeArgs + "--PublishSettings=$PublishSettings"
}
if ($DeploymentEnvironment) {
    $cakeArgs = $cakeArgs + "--DeploymentEnvironment=$DeploymentEnvironment"
}

Write-Host "Executing the following command:" -ForegroundColor Green
Write-Host "dotnet cake .\build\build.cake $cakeArgs" -ForegroundColor Green
dotnet cake .\build\build.cake  $cakeArgs

if ($LASTEXITCODE -ne 0) {
    exit $LASTEXITCODE
}
