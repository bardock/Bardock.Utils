using System;
using System.Collections.Concurrent;

namespace Bardock.Utils.Sync
{
    /// <summary>
    /// Creates objects based on instances of T that can be used to acquire an exclusive lock.
    /// Instanciate one factory for every use case you might have.
    /// Inspired by Eugene Beresovsky solution: http://stackoverflow.com/a/19375402
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LockeableObjectFactory<T> where T : ICloneable
    {
        private readonly ConcurrentDictionary<T, T> _lockeableObjects = new ConcurrentDictionary<T, T>();

        /// <summary>
        /// Returns always the same instance by the specified string
        /// </summary>
        /// <param name="name">The name of the object</param>
        public T Get(T instance)
        {
            return _lockeableObjects.GetOrAdd(instance, valueFactory: x => (T)x.Clone());
        }
    }
}