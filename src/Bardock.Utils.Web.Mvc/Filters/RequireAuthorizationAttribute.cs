using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Bardock.Utils.Web.Extensions;

namespace Bardock.Utils.Web.Mvc.Filters
{
    public class RequireAuthorization : AuthorizeAttribute
    {
        /// <summary>
        /// If this flag is true, provides a Forbidden response when user is authenticated but not authorized
        /// </summary>
        public bool OnlyAuthenticate { get; set; }

        public RequireAuthorization() : base()
        {
            this.OnlyAuthenticate = false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (this.OnlyAuthenticate && filterContext.HttpContext.Request.IsAuthenticated)
            {
                // User is authenticated but not authorized
                // More info: http://stackoverflow.com/questions/238437/why-does-authorizeattribute-redirect-to-the-login-page-for-authentication-and-au

                filterContext.Result = new HttpStatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
            }
            else
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    // When it is a ajax request, avoid redirect to login by forms authentication (or any http module) 
                    filterContext.HttpContext.SkipAuthorization = true;
                    filterContext.HttpContext.Response.Clear();
                    filterContext.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                    filterContext.Result = new HttpUnauthorizedResult();
                    filterContext.Result.ExecuteResult(filterContext.Controller.ControllerContext);
                    filterContext.HttpContext.Response.End();
                }
                else if(FormsAuthentication.IsEnabled)
                {
                    // Redirect to login in order to support MVC areas and avoid FormsAuthenticatin redirect

                    // Parse login url in order to get controller and action parts
                    string loginFullUrl = UriHelper.Absolute(FormsAuthentication.LoginUrl, true).ToString();
                    var routeData = RouteTable.Routes.GetRouteData(loginFullUrl);

                    // Redirect building a RouteValueDictionary (area is inferred from current context)
                    filterContext.Result = new RedirectToRouteResult(
                        new System.Web.Routing.RouteValueDictionary
                        {
                            { "langCode", filterContext.RouteData.Values[ "langCode" ] },
                            { "controller", routeData.Values["controller"] },
                            { "action", routeData.Values["action"] },
                            { "returnUrl", filterContext.HttpContext.Request.RawUrl }
                        });
                }
                else
                {                    
                    base.HandleUnauthorizedRequest(filterContext);
                }
            }
        }

    }
}