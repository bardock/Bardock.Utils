using System;

namespace Bardock.Utils.Logger
{
    /// <summary>
    /// A factory for NullLog
    /// </summary>
    public class NullLogFactory : ILogFactory
    {
        public ILog GetLog(string name)
        {
            return new NullLog();
        }
    }
}
