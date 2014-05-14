using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Bardock.Utils.Web.Mvc.Filters
{
    public class HttpAccessCodeAttribute : ActionFilterAttribute
    {
        public string Code { get; set; }
        public string ParamName { get; set; }

        public HttpAccessCodeAttribute()
        {
            ParamName = "accessCode";
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (IsValidAccessCode(actionContext))
            {
                base.OnActionExecuting(actionContext);
                return;
            }

            OnError(actionContext);
        }

        public override Task OnActionExecutingAsync(HttpActionContext actionContext, System.Threading.CancellationToken cancellationToken)
        {
            if (IsValidAccessCode(actionContext))
                return base.OnActionExecutingAsync(actionContext, cancellationToken);

            OnError(actionContext);

            return Task.FromResult(0);
        }

        protected virtual bool IsValidAccessCode(HttpActionContext actionContext)
        {
            return HttpContext.Current.IsDebuggingEnabled
                || (!string.IsNullOrWhiteSpace(this.Code)
                    && !string.IsNullOrWhiteSpace(this.ParamName)
                    && HttpContext.Current.Request[this.ParamName] == this.Code
                   );
        }

        protected virtual void OnError(HttpActionContext actionContext)
        {
            throw new HttpException(
                (int)HttpStatusCode.NotFound,
                string.Empty
            );
        }
    }
}
