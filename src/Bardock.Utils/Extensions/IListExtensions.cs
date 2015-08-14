using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Bardock.Utils.Extensions
{
    public static class IListExtensions
    {

        /// <summary>
        /// Adds an item to the List
        /// </summary>
        public static IList<TSource> AddItem<TSource>(this IList<TSource> source, TSource item)
        {
            source.Add(item);
            return source;
        }

        /// <summary>
        /// Inserts an item to the List at the specified
        //  index
        /// </summary>
        public static IList<TSource> InsertItem<TSource>(this IList<TSource> source, TSource item, int index)
        {
            source.Insert(index, item);
            return source;
        }
    }
}
