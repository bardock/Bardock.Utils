using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Bardock.Utils.Extensions
{
    public static class WebRequestExtensions
    {
        public static string ReadAllResponseString(this WebRequest request)
        {
            using(var response = request.GetResponse())
            {
                using(var responseStream = response.GetResponseStream())
                {
                    using(var reader = new StreamReader(responseStream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }
    }
}
