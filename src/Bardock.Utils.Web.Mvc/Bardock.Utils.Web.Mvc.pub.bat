:: nuget spec
set /p version=Version number:
nuget pack Bardock.Utils.Web.Mvc.csproj -Prop Configuration=Debug -Symbols -IncludeReferencedProjects -Version %version%
nuget push Bardock.Utils.Web.Mvc.%version%.nupkg
pause;