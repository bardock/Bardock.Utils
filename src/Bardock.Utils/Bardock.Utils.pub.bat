:: nuget spec
set "project=Bardock.Utils"
set /p version=Version number:
nuget pack %project%.csproj -Prop Configuration=Debug -Symbols -IncludeReferencedProjects -Version %version%
nuget push %project%.%version%.nupkg
pause;