using Bardock.Utils.Logger;
using Bardock.Utils.Web.Mvc.Extensions;
using System.IO;
using System.Web.Http;

namespace Bardock.Utils.Web.Mvc
{
    public abstract class BaseApiController : ApiController
    {
        protected RequestNotifications Notifications
        {
            get { return RequestNotifications.Instance; }
        }

		protected ILog Log 
        {
			get { return Logger.Manager.GetLog(this); }
		}
    }
}
