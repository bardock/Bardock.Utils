using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Bardock.Utils.Web.Filters;

namespace Bardock.Utils.Web.WebApi.Filters
{
    public class AccessCodeAttribute : ActionFilterAttribute
    {
        public AccessCodeFilter Filter { get; set; }

        public AccessCodeAttribute()
	    {
            this.Filter = new AccessCodeFilter();
	    }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!Filter.IsValidAccessCode())
            {
                OnError(actionContext);
                return;
            }
            base.OnActionExecuting(actionContext);
        }

        public override Task OnActionExecutingAsync(HttpActionContext actionContext, System.Threading.CancellationToken cancellationToken)
        {
            if (Filter.IsValidAccessCode())
                return base.OnActionExecutingAsync(actionContext, cancellationToken);

            OnError(actionContext);

            return Task.FromResult(0);
        }

        protected virtual void OnError(HttpActionContext actionContext)
        {
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }
    }
}
