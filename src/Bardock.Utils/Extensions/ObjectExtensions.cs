using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Bardock.Utils.Extensions
{
	public static class ObjectExtensions
	{
        public static bool In<T>(this T o, IEnumerable<T> evaluate)
        {
            return o != null && evaluate.Any(x => o.Equals(x));
        }
        public static bool In<T>(this T o, params T[] evaluate)
        {
            return o.In(evaluate as IEnumerable<T>);
        }
        public static TValue TryGetValue<TObject, TValue>(this TObject @this, Func<TObject, TValue> f)
        {
            try
            {
                return f(@this);
            }
            catch
            {
                return default(TValue);
            }
        }
	}
}