:: nuget spec
set /p version=Version number:
nuget pack Bardock.Utils.Web.Mvc.HtmlTags.csproj -Prop Configuration=Debug -Symbols -IncludeReferencedProjects -Version %version%
nuget push Bardock.Utils.Web.Mvc.HtmlTags.%version%.nupkg
pause;