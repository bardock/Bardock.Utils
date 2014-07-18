using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

namespace Bardock.Utils.Extensions
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Filters a sequence of values based on a predicate if specified condition is true.
        /// This methods allows to use method chaining when we must to apply a filter based on a condition that cannot be part of the predicate (i.e. performance issues or query builder does not support it).
        /// For example: myList.Where(...).Where(x => ids.Contains(x.ID), when: ids != null).Select(...)
        /// </summary>
        public static IEnumerable<TSource> Where<TSource>(
            this IEnumerable<TSource> source,
            bool when,
            Func<TSource, bool> predicate)
        {
            if (!when)
                return source;

            return source.Where(predicate);
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate selected based on a specified condition.
        /// This methods allows to use method chaining when we must to apply a filter based on a condition that cannot be part of the predicate (i.e. performance issues or query builder does not support it).
        /// For example: myList.Where(...).Where(when: ids != null, predicate: x => ids.Contains(x.ID), else: x => true).Select(...)
        /// </summary>
        public static IEnumerable<TSource> Where<TSource>(
            this IEnumerable<TSource> source,
            bool when,
            Func<TSource, bool> predicate,
            Func<TSource, bool> elsePredicate)
        {
            return source.Where(when ? predicate : elsePredicate);
        }

        /// <summary>
        /// Splits a sequence of values based on a predicate.
        /// </summary>
        public static IEnumerable<IEnumerable<TSource>> Split<TSource>(
            this IEnumerable<TSource> source, 
            Func<TSource, bool> condition, 
            bool clearEmpty = false)
		{
			var resultList = new List<List<TSource>>();
			var currentList = new List<TSource>();

			foreach (var x in source) 
            {
				if (condition(x)) 
                {
					// If item satisfies condition, ignore it and 
					// add current list to results
					if (!clearEmpty || currentList.Count > 0) 
                    {
						resultList.Add(currentList);
					}
					currentList = new List<TSource>();
				} 
                else 
                {
					// Otherwise, add item to current list
					currentList.Add(x);
				}
			}

			if (!clearEmpty || currentList.Count > 0) 
            {
				resultList.Add(currentList);
			}

			return resultList;
        }

        public static IEnumerable<TSource> ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            source.ToList().ForEach(action);
            return source;
        }

        public static bool ContainsAny<TSource>(this IEnumerable<TSource> source, params TSource[] items)
        {
            return items.Any(item => source.Contains(item));
        }
	}
}