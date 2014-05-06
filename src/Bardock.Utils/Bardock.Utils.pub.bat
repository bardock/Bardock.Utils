:: nuget spec
set /p version=Version number:
nuget pack Bardock.Utils.csproj -Prop Configuration=Release -IncludeReferencedProjects -Version %version%
nuget push Bardock.Utils.%version%.nupkg
pause;