:: nuget spec
set /p version=Version number:
nuget pack Bardock.Utils.UnitTest.Data.csproj -Prop Configuration=Debug -Symbols -IncludeReferencedProjects -Version %version%
nuget push Bardock.Utils.UnitTest.Data.%version%.nupkg
pause;