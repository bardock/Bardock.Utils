:: nuget spec
set /p version=Version number:
nuget pack Bardock.Utils.UnitTest.Data.EF.csproj -Prop Configuration=Debug -Symbols -IncludeReferencedProjects -Version %version%
nuget push Bardock.Utils.UnitTest.Data.EF.%version%.nupkg
pause;