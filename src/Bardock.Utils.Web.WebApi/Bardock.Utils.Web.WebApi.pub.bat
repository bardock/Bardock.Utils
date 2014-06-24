:: nuget spec
set /p version=Version number:
nuget pack Bardock.Utils.Web.WebApi.csproj -Prop Configuration=Debug -Symbols -IncludeReferencedProjects  -Version %version%
nuget push Bardock.Utils.Web.WebApi.%version%.nupkg
pause;