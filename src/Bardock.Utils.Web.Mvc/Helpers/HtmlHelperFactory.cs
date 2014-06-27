using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Bardock.Utils.Web.Mvc.Helpers
{
    public class HtmlHelperFactory
    {
        public static HtmlHelper CreateInstance()
        {
            return CreateInstance(null);
        }

        public static HtmlHelper CreateInstance(object model)
        {
            return CreateInstance(model, new RouteData());
        }

        public static HtmlHelper CreateInstance(object model, RouteData route)
        {
            return CreateInstance(model, route, new ControllerDummy());
        }

        public static HtmlHelper CreateInstance(object model, RouteData route, Controller controller)
        {
            var httpContext = new HttpContextDummy();
            var controllerContext = new ControllerContext(httpContext, route, controller);
            var viewData = new ViewDataDictionary(model);
            var writer = new System.IO.StringWriter();
            ViewContext viewContext = new ViewContext(controllerContext, new ViewDummy(), viewData, new TempDataDictionary(), writer);
            return new HtmlHelper(viewContext, new ViewDataContainerDummy(viewData), new RouteCollection());
        }

        public static HtmlHelper<TModel> CreateInstance<TModel>()
        {
            return CreateInstance<TModel>(default(TModel));
        }

        public static HtmlHelper<TModel> CreateInstance<TModel>(TModel model)
        {
            return CreateInstance<TModel>(model, new RouteData());
        }

        public static HtmlHelper<TModel> CreateInstance<TModel>(TModel model, RouteData route)
        {
            return CreateInstance<TModel>(model, route, new ControllerDummy());
        }

        public static HtmlHelper<TModel> CreateInstance<TModel>(TModel model, RouteData route, Controller controller)
        {
            var httpContext = new HttpContextDummy();
            var controllerContext = new ControllerContext(httpContext, route, controller);
            var viewData = new ViewDataDictionary(model);
            var writer = new System.IO.StringWriter();
            ViewContext viewContext = new ViewContext(controllerContext, new ViewDummy(), viewData, new TempDataDictionary(), writer);
            return new HtmlHelper<TModel>(viewContext, new ViewDataContainerDummy(viewData), new RouteCollection());
        }

        private class ControllerDummy : Controller
        {
        }

        private class HttpRequestDummy : HttpRequestBase
        {
            public override string ApplicationPath
            {
                get { return ""; }
            }

            public override string AppRelativeCurrentExecutionFilePath
            {
                // Any shorter string here gives exception:
                // index larger than length of string
                get { return "~/"; }
            }

            public override string PathInfo
            {
                get { return ""; }
            }
        }

        private class HttpResponseDummy : HttpResponseBase
        {
            public override string ApplyAppPathModifier(string virtualPath)
            {
                return virtualPath;
            }
        }

        private class HttpContextDummy : HttpContextBase
        {
            public override HttpRequestBase Request
            {
                get { return new HttpRequestDummy(); }
            }

            public override HttpResponseBase Response
            {
                get { return new HttpResponseDummy(); }
            }

            public override IDictionary Items 
            { 
                get { return new Dictionary<object, object>(); }
            }
        }

        private class ViewDummy : IView
        {
            public void Render(ViewContext viewContext, System.IO.TextWriter writer)
            {
                throw new NotImplementedException();
            }
        }

        private class ViewDataContainerDummy : IViewDataContainer
        {
            public ViewDataContainerDummy()
            {
            }

            public ViewDataContainerDummy(ViewDataDictionary dataDictionary)
            {
                _data = dataDictionary;
            }

            private ViewDataDictionary _data;
            public ViewDataDictionary ViewData
            {
                get { return _data; }
                set { _data = value; }
            }
        }
    }
}
