:: nuget spec
nuget pack -sym Bardock.Utils.Logger.Log4net.csproj -Prop Configuration=Release -IncludeReferencedProjects
set /p version=Version number:
nuget push Bardock.Utils.Logger.Log4net.%version%.nupkg
pause;