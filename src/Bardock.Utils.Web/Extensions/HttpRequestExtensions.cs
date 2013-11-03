using System.IO;
using System.Runtime.CompilerServices;
using System.Web;

namespace Bardock.Utils.Web.Extensions
{
    public static class HttpRequestExtensions
    {
        public static bool IsAjax(this HttpRequest request)
        {
            return (request["X-Requested-With"] == "XMLHttpRequest") 
                || ((request.Headers != null) && (request.Headers["X-Requested-With"] == "XMLHttpRequest"));
        }
	}

}