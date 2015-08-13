using System;
using System.Collections.Generic;
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
        /// Filters a sequence of values based on a specified range of values of the selected property
        /// </summary>
        public static IEnumerable<TSource> Where<TSource, TProp>(
            this IEnumerable<TSource> source,
            Func<TSource, TProp> selector,
            IEnumerable<TProp> items)
        {
            if (items == null)
                return source;

            //store evaluated collection of items in order to prevent multiple evaluations into the where clause
            var collection = items.ToArray();

            return source.Where(x => collection.Contains(selector(x)));
        }

        /// <summary>
        /// Filters a sequence of values based on a specified range of values of the selected property
        /// </summary>
        public static IEnumerable<TSource> Where<TSource, TProp>(
            this IEnumerable<TSource> source,
            Func<TSource, TProp?> selector,
            IEnumerable<TProp> items)
            where TProp : struct
        {
            if (items == null)
                return source;

            //store evaluated collection of items in order to prevent multiple evaluations into the where clause
            var collection = items.ToArray();

            return source.Where(x => selector(x).HasValue && collection.Contains(selector(x).Value));
        }

        /// <summary>
        /// Filters a sequence of values based on a specified range of values of the selected property
        /// </summary>
        public static IEnumerable<TSource> Where<TSource, TProp>(
            this IEnumerable<TSource> source,
            Func<TSource, TProp> selector,
            IEnumerable<TProp?> items)
            where TProp : struct
        {
            if (items == null)
                return source;

            return source.Where(selector, items.Where(x => x.HasValue).Select(x => x.Value));
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

        /// <summary>
        /// Apply an action to each element.
        /// </summary>
        public static IEnumerable<TSource> ForEach<TSource>(
            this IEnumerable<TSource> source,
            Action<TSource> action)
        {
            return source.ForEach((e, i) => action(e));
        }

        public static IEnumerable<TSource> ForEach<TSource>(
            this IEnumerable<TSource> source,
            Action<TSource, int> action)
        {
            var i = 0;
            foreach (var e in source)
            {
                action(e, i);
                i++;
            }
            return source;
        }

        /// <summary>
        /// Determines whether a sequence contains any of the items
        /// </summary>
        public static bool ContainsAny<TSource>(this IEnumerable<TSource> source, params TSource[] items)
        {
            return items.Any(item => source.Contains(item));
        }

        /// <summary>
        /// Determines whether a sequence contains all of the items
        /// </summary>
        public static bool ContainsAll<TSource>(this IEnumerable<TSource> source, params TSource[] items)
        {
            return items.All(item => source.Contains(item));
        }

        /// <summary>
        /// Adds an item to the end of the sequence
        /// </summary>
        public static IEnumerable<TSource> AddItem<TSource>(
            this IEnumerable<TSource> source,
            TSource item)
        {
            return source.Concat(new TSource[] { item });
        }

        /// <summary>
        /// Adds an item to the specific index of the sequence
        /// </summary>
        public static IEnumerable<TSource> InsertItem<TSource>(
            this IEnumerable<TSource> source,
            TSource item,
            int index)
        {
            index = index - 1 > 0
                        ? index - 1
                        : 0;

            return source
                    .Take(index)
                    .AddItem(item)
                    .Concat(source.Skip(index));
        }
    }
}