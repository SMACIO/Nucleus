Function Rename-Directories
{
	param (
		[string]$Path,
		[string]$Replace,
		[string]$With
	)

	Get-ChildItem -Path $Path -Directory -Recurse | ForEach-Object {
		if ($_.Name.StartsWith($Replace)) {
			$newName = ($_.FullName -replace $Replace, $With)
	
            Rename-Item $_.FullName $newName
			Write-Output $oldPath
			Write-Output $newName
		}
    #>
}
}

Function Rename-Files
{
	param (
		[string]$Path,
		[string]$OfType,
		[string]$Replace,
		[string]$With
	)

    Write-Output $Path

	Ls $Path -Recurse -Include $OfType | ForEach-Object {
		$oldPath = $_.FullName
		$name = $_.Name
		$newName = ($_.Name.Replace($Replace, $With))
		Write-Output $oldPath
		Write-Output $newName

		Rename-Item $oldPath $newName
	}
}

function Replace-Content
{
	param (
		[string]$Path,
		[string]$OfType,
		[string]$Replace,
		[string]$With
	)

	Ls $Path -Recurse -Include $OfType | ForEach-Object {
		#Write-Output $_.FullName
		$fileText = Get-Content $_ -Raw -Encoding UTF8
	
		if ($fileText.Length -gt 0 -and ($fileText.contains($Replace))) {
			$fileText.Replace($Replace,$With) | Set-Content $_ -Encoding UTF8
			Write-Host 'file(change text) ' $_.FullName
		}
	}
}

function Patch-Project
{
    param (
        [string]$path,
        [string]$projectName
    )

    $projectDir = [IO.Path]::Combine($path, "src", $projectName)
    Write-Output $projectDir

    Replace-Content -Path $projectDir -OfType "*.cs" -Replace "using Volo.Abp" -With "using Nucleus"
    Replace-Content -Path $projectDir -OfType "*.cs" -Replace "namespace Volo.Abp" -With "namespace Nucleus"
    Replace-Content -Path $projectDir -OfType "*.cs" -Replace "class Abp" -With "class Nucleus"
    Replace-Content -Path $projectDir -OfType "*.cs" -Replace "interface IAbp" -With "interface INucleus"
    Replace-Content -Path $projectDir -OfType "*.cs" -Replace ": Abp" -With ": Nucleus"
    Replace-Content -Path $projectDir -OfType "*.cs" -Replace ": IAbp" -With ": INucleus"
    Replace-Content -Path $projectDir -OfType "*.cs" -Replace "<Abp" -With "<Nucleus"
    Replace-Content -Path $projectDir -OfType "*.cs" -Replace "<IAbp" -With "<INucleus"
    Replace-Content -Path $projectDir -OfType "*.cs" -Replace "new Abp" -With "new Nucleus"
    Replace-Content -Path $projectDir -OfType "*.cs" -Replace ", Abp" -With ", Nucleus"
    Replace-Content -Path $projectDir -OfType "*.cs" -Replace "AddCoreAbpServices" -With "AddCoreNucleusServices"
    Replace-Content -Path $projectDir -OfType "*.cs" -Replace " Abp" -With " Nucleus"
    Replace-Content -Path $projectDir -OfType "*.cs" -Replace " IAbp" -With " INucleus"
    Replace-Content -Path $projectDir -OfType "*.cs" -Replace ", Abp" -With ", Nucleus"
    Replace-Content -Path $projectDir -OfType "*.cs" -Replace "AbpModule" -With "NucleusModule"
    Replace-Content -Path $projectDir -OfType "*.cs" -Replace "Abp" -With "Nucleus"
    Replace-Content -Path $projectDir -OfType "*.cs" -Replace " abp" -With " nucleus"
    Replace-Content -Path $projectDir -OfType "*.cs" -Replace "(abp" -With "(nucleus"
    Replace-Content -Path $projectDir -OfType "*.cs" -Replace " ABP " -With " NUCLEUS "
    Replace-Content -Path $projectDir -OfType "*.cs" -Replace "[EventName(""abp." -With "[EventName(""nucleus."
    Replace-Content -Path $projectDir -OfType "*.cs" -Replace "_abp" -With "_nucleus"
    Replace-Content -Path $projectDir -OfType "*.cs" -Replace "(""abp." -With "(""nucleus."
    Replace-Content -Path $projectDir -OfType "*.cs" -Replace "('abp." -With "('nucleus."
    Replace-Content -Path $projectDir -OfType "*.cs" -Replace "if (!abp" -With "if (!nucleus"
    Replace-Content -Path $projectDir -OfType "*.cs" -Replace "[Area(""abp" -With "[Area(""nucleus"
    Replace-Content -Path $projectDir -OfType "*.cs" -Replace "Name = ""abp""" -With "Name = ""nucleus"""
    Replace-Content -Path $projectDir -OfType "*.cs" -Replace "[Route(""api/abp" -With "[Route(""api/nucleus"
    Replace-Content -Path $projectDir -OfType "*.cs" -Replace """api/abp" -With """api/nucleus"
    Replace-Content -Path $projectDir -OfType "*.cs" -Replace "/* TODO: The code below is from old ABP, not implemented yet" -With "/*"
    Replace-Content -Path $projectDir -OfType "*.cs" -Replace """Volo." -With """Nucleus."
    Replace-Content -Path $projectDir -OfType "*.cs" -Replace "Volo." -With "Nucleus."
    Replace-Content -Path $projectDir -OfType "*.cs" -Replace "/Volo/Nucleus" -With "/Nucleus"
    Replace-Content -Path $projectDir -OfType "*.cs" -Replace """Nucleus.Nucleus" -With """Nucleus"
    Replace-Content -Path $projectDir -OfType "*.cs" -Replace "Nucleus.Nucleus" -With "Nucleus"
    Replace-Content -Path $projectDir -OfType "*.cs" -Replace """Volo/" -With """Nucleus/"
    Replace-Content -Path $projectDir -OfType "*.cs" -Replace "Nucleus/Nucleus" -With "Nucleus"
    
    Rename-Files -Path $projectDir -OfType "*.cs" -Replace "Abp" -With "Nucleus"
}

 #Rename-Files -Path $rootDir -OfType "*.sln.*" -Replace $voloAbp -With $stUcore

$rootDir = "../Nucleus"
$project = "Nucleus.Core"
$project = "Nucleus.Ddd.Domain"
$project = "Nucleus.Data"
$project = "Nucleus.EventBus"
$project = "Nucleus.Guids"
$project = "Nucleus.MultiTenancy"
$project = "Nucleus.Threading"
$project = "Nucleus.Timing"
$project = "Nucleus.Uow"
$project = "Nucleus.ObjectExtending"
$project = "Nucleus.ObjectMapping"
$project = "Nucleus.ExceptionHandling"
$project = "Nucleus.Localization"
$project = "Nucleus.Localization.Abstractions"
$project = "Nucleus.Specifications"
$project = "Nucleus.Auditing"
$project = "Nucleus.Auditing.Contracts"
$project = "Nucleus.VirtualFileSystem"
$project = "Nucleus.Settings"
$project = "Nucleus.Security"
$project = "Nucleus.EventBus.Abstractions"
$project = "Nucleus.Validation"
$project = "Nucleus.Validation.Abstractions"
$project = "Nucleus.Json"
$project = "Nucleus.DistributedLocking"
$project = "Nucleus.DistributedLocking.Abstractions"
$project = "Nucleus.Timing"
$project = "Nucleus.BackgroundWorkers"
$project = "Nucleus.AspNetCore"
$project = "Nucleus.Http"
$project = "Nucleus.Minify"
$project = "Nucleus.Http.Abstractions"
$project = "Nucleus.Authorization"
$project = "Nucleus.Authorization.Abstractions"
$project = "Nucleus.AspNetCore.Authentication.JwtBearer"
$project = "Nucleus.AspNetCore.Authentication.OAuth"
$project = "Nucleus.AspNetCore.Authentication.OpenIdConnect"
$project = "Nucleus.AspNetCore.MultiTenancy"
$project = "Nucleus.AspNetCore.Mvc"
$project = "Nucleus.Ddd.Application"
$project = "Nucleus.Features"
$project = "Nucleus.AspNetCore.Mvc.Client.Common"
$project = "Nucleus.AspNetCore.Mvc.Client"
$project = "Nucleus.AspNetCore.Mvc.Contracts"
$project = "Nucleus.GlobalFeatures"
$project = "Nucleus.Ddd.Application.Contracts"
$project = "Nucleus.Caching"
$project = "Nucleus.Caching.StackExchangeRedis"
$project = "Nucleus.Serialization"
$project = "Nucleus.Castle.Core"
$project = "Nucleus.ApiVersioning.Abstractions"
$project = "Nucleus.UI"
$project = "Nucleus.UI.Navigation"

$project = "Nucleus.Localization"
#volo

Patch-Project -path $rootDir -projectName $project


#Rename-Directories -Path $rootDir -Replace "Abp" -With "Nucleus"
#Rename-Files -Path  -OfType "*.json" -Replace "abp" -With "nucleus"
#Replace-Content -Path "..\Nucleus\src\Nucleus.AspNetCore.Mvc.Client.Common\ClientProxies" -OfType "*.json" -Replace "abp" "nucleus"
#Replace-Content -Path "..\Nucleus\src\Nucleus.AspNetCore.Mvc.Client.Common\ClientProxies" -OfType "*.json" -Replace "Abp" "Nucleus"

$projects = @(
    "Nucleus.Core",
    "Nucleus.Ddd.Domain",
    "Nucleus.Data",
    "Nucleus.EventBus",
    "Nucleus.Guids",
    "Nucleus.MultiTenancy",
    "Nucleus.Threading",
    "Nucleus.Timing",
    "Nucleus.Uow",
    "Nucleus.ObjectExtending",
    "Nucleus.ObjectMapping",
    "Nucleus.ExceptionHandling",
    "Nucleus.Localization",
    "Nucleus.Localization.Abstractions",
    "Nucleus.Specifications",
    "Nucleus.Auditing",
    "Nucleus.Auditing.Contracts",
    "Nucleus.VirtualFileSystem",
    "Nucleus.Settings",
    "Nucleus.Security",
    "Nucleus.EventBus.Abstractions",
    "Nucleus.Json"
)


