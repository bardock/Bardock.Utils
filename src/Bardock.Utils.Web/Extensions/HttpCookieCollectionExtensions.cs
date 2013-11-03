using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Web;

namespace Bardock.Utils.Web.Extensions
{
    public static class HttpCookieCollectionExtensions
    {
        public static void Add(this HttpCookieCollection cookies, string name, string value, DateTime expires, bool encode = true)
        {
            value = encode ? HttpUtility.UrlEncode(value) : value;
            cookies.Add(
                new HttpCookie(name, value) { Expires = expires }
            );
        }
        public static void Set(this HttpCookieCollection cookies, string name, string value, DateTime expires, bool encode = true)
        {
            value = encode ? HttpUtility.UrlEncode(value) : value;
            cookies.Set(
                new HttpCookie(name, value) { Expires = expires }
            );
        }

        public static void Expire(this HttpCookieCollection cookies, string name)
        {
            cookies.Add(name, string.Empty, DateTime.Now.AddDays(-1));
        }

        public static string GetValue(this HttpCookieCollection cookies, string name, bool decode = true)
        {
            var cookie = cookies[name];

            if(cookie == null)
                return null;

            return decode ? HttpUtility.UrlDecode(cookie.Value) : cookie.Value;
        }
	}

}