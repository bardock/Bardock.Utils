:: nuget spec
nuget pack -sym Bardock.Utils.Logger.Log4net.csproj -Prop Configuration=Release -IncludeReferencedProjects
nuget push Bardock.Utils.Logger.Log4net.1.0.0.0.nupkg
pause;