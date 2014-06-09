:: nuget spec
set /p version=Version number:
nuget pack Bardock.Utils.Web.csproj -Prop Configuration=Release -IncludeReferencedProjects -Version %version%
nuget push Bardock.Utils.Web.%version%.nupkg
pause;