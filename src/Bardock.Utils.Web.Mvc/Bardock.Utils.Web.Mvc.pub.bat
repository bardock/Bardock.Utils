:: nuget spec
nuget pack -sym Bardock.Utils.Web.Mvc.csproj -Prop Configuration=Release -IncludeReferencedProjects
set /p version=Version number:
nuget push Bardock.Utils.Web.Mvc.%version%.nupkg
pause;