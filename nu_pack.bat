set dist_folder=packages-dist
set csproj_config=Release
set csproj_platform=AnyCPU 

echo Cleaning dist folder %dist_folder%
rmdir /S /Q %dist_folder%
mkdir %dist_folder%

nuget pack ./LightDataInterface/LightDataInterface.csproj -OutputDirectory ./%dist_folder% -Prop Configuration=%csproj_config% -Prop Platform=%csproj_platform% -IncludeReferencedProjects
nuget pack ./LightDataInterface.Core/LightDataInterface.Core.csproj -OutputDirectory ./%dist_folder% -Prop Configuration=%csproj_config% -Prop Platform=%csproj_platform% -IncludeReferencedProjects
nuget pack ./LightDataInterface.EntityFramework/LightDataInterface.EntityFramework.csproj -OutputDirectory ./%dist_folder% -Prop Configuration=%csproj_config% -Prop Platform=%csproj_platform% -IncludeReferencedProjects
nuget pack ./LightDataInterface.NHibernate/LightDataInterface.NHibernate.csproj -OutputDirectory ./%dist_folder% -Prop Configuration=%csproj_config% -Prop Platform=%csproj_platform% -IncludeReferencedProjects
nuget pack ./LightDataInterface.Extra.WebApi/LightDataInterface.Extra.WebApi.csproj -OutputDirectory ./%dist_folder% -Prop Configuration=%csproj_config% -Prop Platform=%csproj_platform% -IncludeReferencedProjects
