using Bardock.Utils.Logger;
using Bardock.Utils.Web.Mvc.Extensions;
using System.IO;
using System.Web.Mvc;

namespace Bardock.Utils.Web.Mvc
{
    public abstract class BaseController : Controller
    {    
		protected RequestNotifications Notifications 
        {
            get { return RequestNotifications.Instance; }
		}

		protected ILog Log 
        {
			get { return Logger.Manager.GetLog(this); }
		}

		protected ActionResult RedirectToPreviousLocal()
		{
			object prev = Url.PreviousLocal();
			if ((prev == null)) {
				return RedirectToHome();
			} else {
				return Redirect(prev.ToString());
			}
		}

		protected RedirectResult RedirectToHome()
        {
            return Redirect("~/");
		}

        protected RedirectToRouteResult RedirectToControllerHome()
        {
            return RedirectToAction(string.Empty);
        }

		protected RedirectToRouteResult RedirectToError()
		{
            //TODO: config
			return RedirectToAction("unknown", "error");
		}

		public string RenderViewToString(string viewName, object model)
		{
			ViewData.Model = model;
			using (var sw = new StringWriter()) {
				var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
				var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
				viewResult.View.Render(viewContext, sw);
				viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
				return sw.GetStringBuilder().ToString();
			}
		}
    }
}
