using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Bardock.Utils.Web.Mvc.Filters
{
    public class AccessCodeAttribute : ActionFilterAttribute
    {
        public string Code { get; set; }
        public string ParamName { get; set; }

        public AccessCodeAttribute ()
	    {
            ParamName = "accessCode";
	    }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.IsChildAction 
                || filterContext.HttpContext.IsDebuggingEnabled 
                || (!string.IsNullOrWhiteSpace(this.Code) 
                    && !string.IsNullOrWhiteSpace(this.ParamName)
                    && filterContext.HttpContext.Request[this.ParamName] == this.Code
                   )
                )
                return;

            this.ReturnNotFound(filterContext);
        }

        private void ReturnNotFound(ActionExecutingContext filterContext)
        {
            filterContext.Result = new HttpNotFoundResult("Not Found");
            filterContext.Result.ExecuteResult(filterContext.Controller.ControllerContext);
        }
    }
}
