using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

namespace Bardock.Utils.Extensions
{
    public static class IEnumerableExtensions
	{
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

			foreach (var x in source) {
				if ((condition(x))) {
					// If item satisfies condition, ignore it and 
					// add current list to results
					if ((!clearEmpty || currentList.Count > 0)) {
						resultList.Add(currentList);
					}
					currentList = new List<TSource>();
				} else {
					// Otherwise, add item to current list
					currentList.Add(x);
				}
			}

			if ((!clearEmpty || currentList.Count > 0)) {
				resultList.Add(currentList);
			}

			return resultList;
        }

        public static IEnumerable<TSource> ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            source.ToList().ForEach(action);
            return source;
        }

        public static IEnumerable<TSource> ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource, int> action)
        {
            source.ToList().ForEach(action);
            return source;
        }
	}
}