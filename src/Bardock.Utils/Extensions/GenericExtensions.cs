using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Bardock.Utils.Extensions
{
	public static class GenericExtensions
	{
        /// <summary>
        /// Determines if the instance is equal to any of the specified values
        /// </summary>
        public static bool In<T>(this T @this, IEnumerable<T> evaluate)
        {
            return @this != null && evaluate.Any(x => @this.Equals(x));
        }

        /// <summary>
        /// Determines if the instance is equal to any of the specified values
        /// </summary>
        public static bool In<T>(this T @this, params T[] evaluate)
        {
            return @this.In(evaluate as IEnumerable<T>);
        }
	}
}