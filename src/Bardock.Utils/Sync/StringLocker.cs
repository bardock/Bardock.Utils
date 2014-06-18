using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Bardock.Utils.Sync
{
    /// <summary>
    /// Provides a way to lock based on a string. 
    /// Instanciate one StringLocker for every use case you might have.
    /// </summary>
    public class StringLocker
    {
        // More info: http://stackoverflow.com/a/19375402

        private readonly ConcurrentDictionary<string, string> _locks =
            new ConcurrentDictionary<string, string>();

        public string GetLockObject(string s)
        {
            return _locks.GetOrAdd(s, String.Copy);
        }
    }
}
