:: nuget spec
set /p version=Version number:
nuget pack Bardock.Utils.Web.Mvc.csproj -Prop Configuration=Release -IncludeReferencedProjects -Version %version%
nuget push Bardock.Utils.Web.Mvc.%version%.nupkg
pause;