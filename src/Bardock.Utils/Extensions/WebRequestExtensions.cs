using System.IO;
using System.Net;

namespace Bardock.Utils.Extensions
{
    public static class WebRequestExtensions
    {
        public static string ReadAllResponseString(this WebRequest request)
        {
            using (var response = request.GetResponse())
            {
                using (var responseStream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(responseStream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }
    }
}