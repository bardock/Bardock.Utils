using System;

namespace Bardock.Utils.Extensions
{
    public static class NullableExtensions
    {
        /// <summary>
        /// Applies a specified function if current instance is not null. Otherwise, returns null.
        /// </summary>
        public static TReturn ApplyOrDefault<T, TReturn>(this T? d, Func<T, TReturn> apply) where T : struct
        {
            if (d.HasValue)
                return apply(d.Value);
            return default(TReturn);
        }

        /// <summary>
        /// Applies a specified function if current instance is not null. Otherwise, returns null.
        /// </summary>
        public static TReturn ApplyOrDefault<T, TReturn>(this T d, Func<T, TReturn> apply) where T : class
        {
            if (d != null)
                return apply(d);
            return default(TReturn);
        }
    }
}