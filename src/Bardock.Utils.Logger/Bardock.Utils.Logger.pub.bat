:: nuget spec
nuget pack -sym Bardock.Utils.Logger.csproj -Prop Configuration=Release
nuget push Bardock.Utils.Logger.1.0.0.0.nupkg
pause;