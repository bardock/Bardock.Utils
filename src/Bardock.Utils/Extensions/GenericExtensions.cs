using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Bardock.Utils.Extensions
{
    public static class GenericExtensions
    {
        /// <summary>
        /// Determines if the instance satifies the specified predicated
        /// </summary>
        public static bool Is<T>(this T @this, Func<T, bool> predicate)
        {
            return predicate(@this);
        }

        /// <summary>
        /// Determines if the instance satifies the specified predicated
        /// </summary>
        public static bool Is<T>(this T @this, Expression<Func<T, bool>> predicateExpr)
        {
            return @this.Is(predicateExpr.Compile());
        }

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

        /// <summary>
        /// Applies specified function when the flag is true, otherwise return specified default value
        /// This methods provides method chaining when a condition must be evaluated.
        /// </summary>
        public static TResult Apply<T, TResult>(this T @this, Func<T, TResult> f, bool when, TResult @default = default(TResult))
        {
            return when ? f.Invoke(@this) : @default;
        }

        /// <summary>
        /// Applies specified function when the flag is true.
        /// This methods provides method chaining when a condition must be evaluated.
        /// </summary>
        public static T Apply<T>(this T @this, Func<T, T> f, bool when)
        {
            return when ? f.Invoke(@this) : @this;
        }
    }
}