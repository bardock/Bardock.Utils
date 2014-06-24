using System.Web.Mvc;

namespace Bardock.Utils.Web.Mvc.Filters
{
    public class AccessCodeAttribute : ActionFilterAttribute
    {
        public MvcFilter Filter { get; set; }

        public AccessCodeAttribute ()
	    {
            this.Filter = new MvcFilter();
	    }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Filter.FilterContext = filterContext;

            if (!Filter.IsValidAccessCode())
            {
                OnError(filterContext);
                return;
            }
            base.OnActionExecuting(filterContext);
        }

        protected virtual void OnError(ActionExecutingContext filterContext)
        {
            filterContext.Result = new HttpNotFoundResult("Not Found");
            filterContext.Result.ExecuteResult(filterContext.Controller.ControllerContext);
        }

        public class MvcFilter : Bardock.Utils.Web.Filters.AccessCodeFilter
        {
            public ActionExecutingContext FilterContext { get; set; }

            public override bool IsValidAccessCode()
            {
                return FilterContext.IsChildAction 
                    || base.IsValidAccessCode();
            }

            protected override bool IsDebuggingEnabled()
            {
                return FilterContext.HttpContext.IsDebuggingEnabled;
            }

            protected override string GetRequestCode()
            {
                return FilterContext.HttpContext.Request[this.ParamName];
            }
        }
    }
}
