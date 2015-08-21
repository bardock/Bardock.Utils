using Bardock.Utils.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bardock.Utils.Extensions
{
    public static class IDictionaryExtensions
    {
        public static IDictionary<TKey, TValue> AddItem<TKey, TValue>(
            this IDictionary<TKey, TValue> source,
            TKey key,
            TValue value)
        {
            source.Add(key, value);
            return source;
        }

        public static IDictionary<TKey, TValue> AddItem<TKey, TValue>(
            this IDictionary<TKey, TValue> source,
            bool when,
            TKey key, TValue value)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (when)
                return source.AddItem(key, value);

            return source;
        }

        public static IDictionary<TKey, TValue> Merge<TKey, TValue>(
            this IDictionary<TKey, TValue> source,
            params IDictionary<TKey, TValue>[] args)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (args == null)
                throw new ArgumentNullException("args");

            if (args.Any(arg => arg == null))
                throw new ArgumentException("Parameter args cannot contain null values", "args");

            //copy source dictionary
            var result = source.ToDictionary(x => x.Key, x => x.Value);
            foreach (var arg in args)
            {
                foreach (var kv in arg)
                {
                    if (!result.ContainsKey(kv.Key))
                    {
                        result.Add(kv.Key, kv.Value);
                    }
                    else
                    {
                        result[kv.Key] = kv.Value;
                    }
                }
            }

            return result;
        }

        public static IDictionary<TKey, TValue> Merge<TKey, TValue>(
            this IDictionary<TKey, TValue> source,
            IDictionary<TKey, TValue> arg)
        {
            if (arg == null)
                throw new ArgumentNullException("arg");

            return source.Merge(Coll.Array(arg));
        }
    }
}