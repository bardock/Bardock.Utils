using System.Reflection;
using System.Web;

namespace Bardock.Utils.Web.Extensions
{
    public static class HttpResponseExtensions
    {
        public static bool AreHeadersWritten(this HttpResponse response)
        {
            var type = response.GetType();

            var prop = type.GetProperty("HeadersWritten");
            if (prop == null)
            {
                prop = type.GetProperty("HeadersWritten", BindingFlags.NonPublic | BindingFlags.Instance);
                if (prop == null)
                    return false;
            }

            return (bool)prop.GetValue(response);
        }
    }
}