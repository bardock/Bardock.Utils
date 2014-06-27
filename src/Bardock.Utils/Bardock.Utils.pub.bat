:: nuget spec
set /p version=Version number:
nuget pack Bardock.Utils.csproj -Prop Configuration=Debug -Symbols -IncludeReferencedProjects -Version %version%
nuget push Bardock.Utils.%version%.nupkg
pause;