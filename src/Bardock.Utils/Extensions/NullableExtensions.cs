using System;

namespace Bardock.Utils.Extensions
{
    public static class NullableExtensions
    {
        /// <summary>
        /// Applies a specified function if current instance is not null. Otherwise, returns null.
        /// </summary>
        public static TReturn ApplyOrDefault<T, TReturn>(
            this T? @this, 
            Func<T, TReturn> apply, 
            TReturn defaultValue = default(TReturn)) where T : struct
        {
            return @this.HasValue
                ? apply(@this.Value)
                : defaultValue;
        }

        /// <summary>
        /// Applies a specified function if current instance is not null. Otherwise, returns null.
        /// </summary>
        public static TReturn ApplyOrDefault<T, TReturn>(
            this T @this, 
            Func<T, TReturn> apply,
            TReturn defaultValue = default(TReturn)) where T : class
        {
            return @this != null
                ? apply(@this)
                : defaultValue;
        }
    }
}