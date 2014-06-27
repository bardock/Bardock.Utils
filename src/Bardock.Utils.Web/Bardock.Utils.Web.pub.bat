:: nuget spec
set /p version=Version number:
nuget pack Bardock.Utils.Web.csproj -Prop Configuration=Debug -Symbols -IncludeReferencedProjects -Version %version%
nuget push Bardock.Utils.Web.%version%.nupkg
pause;