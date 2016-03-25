# Bardock Utils [![Build status](https://ci.appveyor.com/api/projects/status/bi7td721qqbra45b?svg=true)](https://ci.appveyor.com/project/bardock/dotnet-utils)

A set of utilities for .NET Framework.

## Components

* [Bardock.Utils](./src/Bardock.Utils) - Core utilities and helpers
* [Bardock.Utils.Data.EF](./src/Bardock.Utils.Data.EF) - Extensions for Entity Framework 6
* [Bardock.Utils.Logger](./src/Bardock.Utils.Logger) - Abstractions for logging. A [log4net implementation](./src/Bardock.Utils.Logger.Log4net) is included
* [Bardock.Utils.Web](./src/Bardock.Utils.Web) - ASP.NET core extensions and helpers
* [Bardock.Utils.Web.Mvc](./src/Bardock.Utils.Web.Mvc) - ASP.NET MVC extensions and helpers
* [Bardock.Utils.Web.Mvc.HtmlTags](./src/Bardock.Utils.Web.Mvc.HtmlTags) - ASP.NET MVC Helpers for generating HTML applying the builder pattern using [HtmlTags](https://github.com/darthfubumvc/htmltags) library
* [Bardock.Utils.Web.WebApi](./src/Bardock.Utils.Web.WebApi) - ASP.NET Web API 2 extensions and helpers

## Related projects

* [Bardock.UnitTesting](https://github.com/bardock/Bardock.UnitTesting) - A framework for unit testing (AutoFixture, Effort and more)
* [Bardock.Caching.Proxies](https://github.com/bardock/Bardock.Caching.Proxies) - Abstracts access to cached items