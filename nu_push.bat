set dist_folder=packages-dist
set nuget_source=https://www.nuget.org
LightDataInterface.0.3.0-alpha.nupkg
nuget push .\%dist_folder%\LightDataInterface.0.3.0-alpha.nupkg -Source %nuget_source%
nuget push .\%dist_folder%\LightDataInterface.EntityFramework.0.3.0-alpha.nupkg -Source %nuget_source%
