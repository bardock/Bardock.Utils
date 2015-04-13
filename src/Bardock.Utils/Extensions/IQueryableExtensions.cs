using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Bardock.Utils.Linq.Expressions;

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
            bool when,
            Expression<Func<TSource, bool>> predicate)
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
        public static IQueryable<TSource> Where<TSource>(
            this IQueryable<TSource> source,
            bool when,
            Expression<Func<TSource, bool>> predicate,
            Expression<Func<TSource, bool>> elsePredicate)
        {
            return source.Where(when ? predicate : elsePredicate);
        }

        /// <summary>
        /// Filters a sequence of values based on specified range of dates.
        /// </summary>
        public static IQueryable<TSource> WhereBetween<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, DateTime>> dateExp,
            DateTime? fromDate,
            DateTime? toDate,
            bool removeTime = false)
        {
            // Build a binary expression with date range evaluation
            BinaryExpression exp = null;

            if (fromDate.HasValue)
            {
                if (removeTime)
                {
                    fromDate = fromDate.Value.ToDayStart();
                }
                exp = Expression.GreaterThanOrEqual(dateExp.Body, Expression.Constant(fromDate.Value));
            }

            if (toDate.HasValue)
            {
                if (removeTime)
                {
                    toDate = toDate.Value.ToDayStart().AddDays(1);
                }
                var exp2 = Expression.LessThan(dateExp.Body, Expression.Constant(toDate.Value));
                if (exp == null)
                {
                    exp = exp2;
                }
                else
                {
                    exp = Expression.AndAlso(exp, exp2);
                }
            }

            // Return original source if dates are both null
            if (exp == null)
            {
                return source;
            }

            Expression<Func<TSource, bool>> predicate = Expression.Lambda<Func<TSource, bool>>(exp, dateExp.Parameters);

            return source.Where(predicate);
        }

        private static readonly MethodInfo _OrderByMethod = typeof(Queryable).GetMethods().Where(method => method.Name == "OrderBy").Where(method => method.GetParameters().Length == 2).Single();
        private static readonly MethodInfo _OrderByDescendingMethod = typeof(Queryable).GetMethods().Where(method => method.Name == "OrderByDescending").Where(method => method.GetParameters().Length == 2).Single();

        /// <summary>
        /// Sorts the elements of a sequence according to a key property specified as string.
        /// </summary>
        public static IOrderedQueryable<TSource> OrderByProperty<TSource>(this IQueryable<TSource> source, string propertyExpression, bool @ascending = true)
        {
            var lambda = ExpressionHelper.ParseProperties<TSource>(propertyExpression);
            var orderByMethod = @ascending ? _OrderByMethod : _OrderByDescendingMethod;
            var genericMethod = orderByMethod.MakeGenericMethod(typeof(TSource), lambda.Body.Type);
            var ret = genericMethod.Invoke(null, new object[] {
		        source,
		        lambda
	        });
            return (IOrderedQueryable<TSource>)ret;
        }

        private static readonly MethodInfo _ThenByMethod = typeof(Queryable).GetMethods().Where(method => method.Name == "ThenBy").Where(method => method.GetParameters().Length == 2).Single();
        private static readonly MethodInfo _ThenByDescendingMethod = typeof(Queryable).GetMethods().Where(method => method.Name == "ThenByDescending").Where(method => method.GetParameters().Length == 2).Single();

        public static IOrderedQueryable<TSource> ThenByProperty<TSource>(this IOrderedQueryable<TSource> source, string propertyExpression, bool @ascending = true)
        {
            var lambda = ExpressionHelper.ParseProperties<TSource>(propertyExpression);
            var orderByMethod = @ascending ? _ThenByMethod : _ThenByDescendingMethod;
            var genericMethod = orderByMethod.MakeGenericMethod(new Type[]{
		        typeof(TSource),
		        lambda.Body.Type
	        });
            var ret = genericMethod.Invoke(null, new object[] {
		        source,
		        lambda
	        });
            return (IOrderedQueryable<TSource>)ret;
        }
    }
}