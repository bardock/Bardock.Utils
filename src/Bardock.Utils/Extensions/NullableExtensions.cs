using System;
using System.Runtime.CompilerServices;

namespace Bardock.Utils.Extensions
{
	public static class NullableExtensions
	{
        public static TReturn ApplyOrDefault<T, TReturn>(this T? d, Func<T, TReturn> apply) where T : struct
        {
            if(d.HasValue)
                return apply(d.Value);
            return default(TReturn);
        }

        public static TReturn ApplyOrDefault<T, TReturn>(this T d, Func<T, TReturn> apply) where T : class
        {
            if(d != null)
                return apply(d);
            return default(TReturn);
        }
	}
}