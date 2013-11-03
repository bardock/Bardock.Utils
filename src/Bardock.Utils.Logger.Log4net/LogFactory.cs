using System;

namespace Bardock.Utils.Logger.Log4net
{
    public class LogFactory : ILogFactory
    {
        public ILog GetLog(Type t)
        {
            return new Log(
                log4net.LogManager.GetLogger(t)
            );
        }
    }
}
