using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

namespace Bardock.Utils.Collections.Extensions
{
    public static class IEnumerableExtensions
	{
        public static IEnumerable<IEnumerable<TSource>> Split<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> condition, bool clearEmpty = false)
		{
			var resultList = new List<List<TSource>>();
			var currentList = new List<TSource>();

			foreach (var x in source) {
				if ((condition(x))) {
					//If item satisfy condition, ignore it and 
					//add current list to results and create a new one
					if ((!clearEmpty || currentList.Count > 0)) {
						resultList.Add(currentList);
					}
					currentList = new List<TSource>();
				} else {
					//Otherwise, add item to current list
					currentList.Add(x);
				}
			}

			if ((!clearEmpty || currentList.Count > 0)) {
				resultList.Add(currentList);
			}

			return resultList;
		}

        public static IEnumerable<TSource> AddIf<TSource>(this IEnumerable<TSource> that, Func<IEnumerable<TSource>, bool> condition)
        {
            var resultList = new List<TSource>();

            foreach (var item in that)
            {
                if (condition(resultList.Concat(Coll.Array(item))))
                {
                    resultList.Add(item);
                }
            }

            return resultList;
        }

	}

}