using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

namespace Bardock.Utils.Extensions
{
    public static class ArrayExtensions
    {
        public static TSource[] ForEach<TSource>(this TSource[] source, Action<TSource, int> action)
        {
            for (int i = 0; i < source.Length; i++)
            {
                action(source[i], i);
            }
            return source;
        }
	}
}