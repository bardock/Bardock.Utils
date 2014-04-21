:: nuget spec
nuget pack -sym Bardock.Utils.csproj -Prop Configuration=Release -IncludeReferencedProjects
nuget push Bardock.Utils.1.1.0.0.nupkg
pause;