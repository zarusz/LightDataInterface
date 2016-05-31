set dist_folder=packages-dist
set nuget_source=https://www.nuget.org
nuget push .\%dist_folder%\LightDataInterface.0.4.*.nupkg -Source %nuget_source%
nuget push .\%dist_folder%\LightDataInterface.Core.0.4.*.nupkg -Source %nuget_source%
nuget push .\%dist_folder%\LightDataInterface.EntityFramework.0.4.*.nupkg -Source %nuget_source%
nuget push .\%dist_folder%\LightDataInterface.NHibernate.0.4.*.nupkg -Source %nuget_source%
nuget push .\%dist_folder%\LightDataInterface.Extra.WebApi.0.4.*.nupkg -Source %nuget_source%
