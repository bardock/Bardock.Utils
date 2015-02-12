:: nuget spec
set /p version=Version number:
nuget pack Bardock.Utils.UnitTest.Data.EF.Effort.DataLoaders.csproj -Prop Configuration=Debug -Symbols -IncludeReferencedProjects -Version %version%
nuget push Bardock.Utils.UnitTest.Data.EF.Effort.DataLoaders.%version%.nupkg
pause;