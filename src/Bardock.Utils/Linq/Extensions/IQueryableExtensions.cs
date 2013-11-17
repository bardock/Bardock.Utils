using System.Runtime.CompilerServices;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;
using System;
using Bardock.Utils.Extensions;
using Bardock.Utils.Linq.Expressions;

namespace Bardock.Utils.Linq.Extensions
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
            if (!condition)
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

        public static IQueryable<TSource> WhereDateRange<TSource>(
                                                    this IQueryable<TSource> source,
                                                    DateTime? fromDate,
                                                    DateTime? toDate,
                                                    Expression<Func<TSource, DateTime>> dateExp)
        {
            //Build a binary expression with date range evaluation
            {
                BinaryExpression exp = null;
                if ((fromDate.HasValue))
                {
                    fromDate = fromDate.Value.ToDayStart();
                    exp = Expression.GreaterThanOrEqual(dateExp.Body, Expression.Constant(fromDate.Value));
                }
                if ((toDate.HasValue))
                {
                    toDate = toDate.Value.ToDayStart().AddDays(1);
                    var exp2 = Expression.LessThan(dateExp.Body, Expression.Constant(toDate.Value));
                    if ((exp == null))
                    {
                        exp = exp2;
                    }
                    else
                    {
                        exp = Expression.AndAlso(exp, exp2);
                    }
                }

                //Return original source if dates are both null
                if ((exp == null))
                {
                    return source;
                }

                //Build "where" predicate
                Expression<Func<TSource, bool>> predicate = Expression.Lambda<Func<TSource, bool>>(exp, dateExp.Parameters);

                //Apply where predicate
                return source.Where(predicate);
            }
        }

        private static readonly MethodInfo _OrderByMethod = typeof(Queryable).GetMethods().Where(method => method.Name == "OrderBy").Where(method => method.GetParameters().Length == 2).Single();
        private static readonly MethodInfo _OrderByDescendingMethod = typeof(Queryable).GetMethods().Where(method => method.Name == "OrderByDescending").Where(method => method.GetParameters().Length == 2).Single();

        public static IOrderedQueryable<TSource> OrderByProperty<TSource>(IQueryable<TSource> source, string propertyExpression, bool @ascending = true)
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
	}	
}