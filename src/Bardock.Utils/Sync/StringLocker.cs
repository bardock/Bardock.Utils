using System;
using System.Collections.Concurrent;

namespace Bardock.Utils.Sync
{
    /// <summary>
    /// Provides a way to lock based on a string.
    /// Instanciate one StringLocker for every use case you might have.
    /// </summary>
    [Obsolete("Please use LockeableObjectFactory instead")]
    public class StringLocker
    {
        private readonly ConcurrentDictionary<string, string> _locks =
            new ConcurrentDictionary<string, string>();

        public string GetLockObject(string s)
        {
            return _locks.GetOrAdd(s, String.Copy);
        }
    }
}