:: nuget spec
nuget pack -sym Bardock.Utils.Web.csproj -Prop Configuration=Release -IncludeReferencedProjects
set /p version=Version number:
nuget push Bardock.Utils.Web.%version%.nupkg
pause;