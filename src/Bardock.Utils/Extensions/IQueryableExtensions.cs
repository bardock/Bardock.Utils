using System;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Bardock.Utils.Extensions
{
    public static class IQueryableExtensions
	{
        /// <summary>
        /// Filters a sequence of values based on a predicate if specified condition is true.
        /// This methods allows to use method chaining when we must to apply a filter based on a condition that cannot be part of the predicate (i.e. performance issues or query builder does not support it).
        /// For example: myQuery.Where(...).WhereIf(ids != null, x => ids.Contains(x.ID)).Select(...)
        /// </summary>
        public static IQueryable<TSource> WhereIf<TSource>(
            this IQueryable<TSource> source, 
            bool condition,
            Expression<Func<TSource, bool>> predicate)
        {
            if(!condition)
                return source;

            return source.Where(predicate);
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate selected based on a specified condition.
        /// This methods allows to use method chaining when we must to apply a filter based on a condition that cannot be part of the predicate (i.e. performance issues or query builder does not support it).
        /// For example: myQuery.Where(...).WhereIf(ids != null, x => ids.Contains(x.ID)).Select(...)
        /// </summary>
        public static IQueryable<TSource> WhereIf<TSource>(
            this IQueryable<TSource> source,
            bool condition,
            Expression<Func<TSource, bool>> predicateTrue,
            Expression<Func<TSource, bool>> predicateFalse)
        {
            return source.Where(condition ? predicateTrue : predicateFalse);
        }
	}
}