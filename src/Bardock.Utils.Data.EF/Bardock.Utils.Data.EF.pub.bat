:: nuget spec
set /p version=Version number:
nuget pack Bardock.Utils.Data.EF.csproj -Prop Configuration=Debug -Symbols -IncludeReferencedProjects -Version %version%
nuget push Bardock.Utils.Data.EF.%version%.nupkg
pause;