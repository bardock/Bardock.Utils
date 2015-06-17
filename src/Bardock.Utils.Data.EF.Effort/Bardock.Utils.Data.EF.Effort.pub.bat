:: nuget spec
set "project=Bardock.Utils.Data.EF.Effort"
set /p version=Version number:
nuget pack %project%.csproj -Prop Configuration=Debug -Symbols -IncludeReferencedProjects -Version %version%
nuget push %project%.%version%.nupkg
pause;