using System;

namespace Bardock.Utils.Logger
{
    /// <summary>
    /// A factory for ILogMock
    /// </summary>
    public class LogFactoryMock : ILogFactory
    {
        public ILog GetLog(Type t)
        {
            return new LogMock();
        }
    }
}
