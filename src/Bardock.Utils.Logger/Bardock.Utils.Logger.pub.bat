:: nuget spec
set /p version=Version number:
nuget pack Bardock.Utils.Logger.csproj -Prop Configuration=Release -IncludeReferencedProjects -Version %version%
nuget push Bardock.Utils.Logger.%version%.nupkg
pause;