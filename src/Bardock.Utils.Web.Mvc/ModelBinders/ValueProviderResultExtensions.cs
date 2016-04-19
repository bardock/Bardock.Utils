using System.Web.Mvc;

namespace Bardock.Utils.Web.Mvc.ModelBinders
{
    public static class ValueProviderResultExtensions
    {
        /// <summary>
        /// Determines if the binding field is present in the context, regardless of its value.
        /// </summary>
        public static bool IsPresent(this ValueProviderResult @this)
        {
            return @this != null;
        }
    }
}