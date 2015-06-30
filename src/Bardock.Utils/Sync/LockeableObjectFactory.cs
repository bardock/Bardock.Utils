using System;
using System.Collections.Concurrent;

namespace Bardock.Utils.Sync
{
    /// <summary>
    /// Creates objects based on instances of TSeed that can be used to acquire an exclusive lock.
    /// Instanciate one factory for every use case you might have.
    /// Inspired by Eugene Beresovsky's solution: http://stackoverflow.com/a/19375402
    /// </summary>
    /// <typeparam name="TSeed">Type of the object you want lock on</typeparam>
    public class LockeableObjectFactory<TSeed>
    {
        private readonly ConcurrentDictionary<TSeed, object> _lockeableObjects = new ConcurrentDictionary<TSeed, object>();

        /// <summary>
        /// Creates or uses an existing object instance by specified seed
        /// </summary>
        /// <param name="seed">
        /// The object used to generate a new lockeable object.
        /// The default EqualityComparer<TSeed> is used to determine if two seeds are equal. 
        /// The same object instance is returned for equal seeds, otherwise a new object is created.
        /// </param>
        public object Get(TSeed seed)
        {
            return _lockeableObjects.GetOrAdd(seed, valueFactory: x => new object());
        }
    }
}