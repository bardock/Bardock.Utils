using System;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Bardock.Utils.Extensions
{
    public static class IQueryableExtensions
	{
        public static IQueryable<TSource> WhereIf<TSource>(
            this IQueryable<TSource> source, 
            bool condition,
            Expression<Func<TSource, bool>> predicate)
        {
            if(!condition)
                return source;

            return source.Where(predicate);
        }
	}
}