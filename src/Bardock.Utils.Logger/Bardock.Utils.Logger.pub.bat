:: nuget spec
set /p version=Version number:
nuget pack Bardock.Utils.Logger.csproj -Prop Configuration=Debug -Symbols -IncludeReferencedProjects -Version %version%
nuget push Bardock.Utils.Logger.%version%.nupkg
pause;