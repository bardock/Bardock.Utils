using System;
using System.Linq;
using System.Linq.Expressions;

namespace Bardock.Utils.Extensions
{
    public static class IQueryableExtensions
    {
        /// <summary>
        /// Filters a sequence of values based on a predicate if specified condition is true.
        /// This methods allows to use method chaining when we must to apply a filter based on a condition that cannot be part of the predicate (i.e. performance issues or query builder does not support it).
        /// For example: myQuery.Where(...).Where(x => ids.Contains(x.ID), when: ids != null).Select(...)
        /// </summary>
        public static IQueryable<TSource> Where<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, bool>> predicate,
            bool when)
        {
            if (!when)
                return source;

            return source.Where(predicate);
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate selected based on a specified condition.
        /// This methods allows to use method chaining when we must to apply a filter based on a condition that cannot be part of the predicate (i.e. performance issues or query builder does not support it).
        /// For example: myQuery.Where(...).Where(when: ids != null, predicate: x => ids.Contains(x.ID), else: x => true).Select(...)
        /// </summary>
        public static IQueryable<TSource> WhereIf<TSource>(
            this IQueryable<TSource> source,
            bool when,
            Expression<Func<TSource, bool>> predicate,
            Expression<Func<TSource, bool>> elsePredicate)
        {
            return source.Where(when ? predicate : elsePredicate);
        }
	}

}