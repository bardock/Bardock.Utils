using System;

namespace Bardock.Utils.Sync
{
    /// <summary>
    /// Provides a way to lock based on a string.
    /// Instanciate one StringLocker for every use case you might have.
    /// </summary>
    [Obsolete("Please use LockeableObjectFactory instead")]
    public class StringLocker : LockeableObjectFactory<string>
    {
        public string GetLockObject(string s)
        {
            return this.Get(s);
        }
    }
}