using System;

namespace Bardock.Utils.Collections
{
    [Obsolete("Please use native array initializers")]
    public class Coll
    {
        /// <summary>
        /// Builds an array based on specified parameters.
        /// Provides a short syntax and type inference.
        /// </summary>
        public static T[] Array<T>()
        {
            return new T[] { };
        }

        /// <summary>
        /// Builds an array based on specified parameters.
        /// Provides a short syntax and type inference.
        /// </summary>
        public static T[] Array<T>(params T[] args)
        {
            return args;
        }
    }
}