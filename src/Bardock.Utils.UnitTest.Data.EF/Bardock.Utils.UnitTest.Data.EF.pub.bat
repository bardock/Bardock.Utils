set "project=Bardock.Utils.UnitTest.Data.EF"
set "localSourcePath=V:\_NuGetPackages"
set /p version=Version number: 

:: create package
nuget pack %project%.csproj -Prop Configuration=Debug -Symbols -IncludeReferencedProjects -Version %version%

:: publish
:: nuget push %project%.%version%.nupkg

:: if you want to publish the package to a local source, comment previous line and uncomment the following lines 
move %project%.%version%.nupkg %localSourcePath%
move %project%.%version%.symbols.nupkg %localSourcePath%

pause;