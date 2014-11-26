:: nuget spec
set /p version=Version number:
nuget pack Bardock.Utils.Logger.Log4net.csproj -Prop Configuration=Debug -Symbols -IncludeReferencedProjects -Version %version%
nuget push Bardock.Utils.Logger.Log4net.%version%.nupkg
pause;