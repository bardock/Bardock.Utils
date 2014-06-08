using log4net.Config;
using System;

namespace Bardock.Utils.Logger.Log4net
{
    public class LogFactory : ILogFactory
    {
        public LogFactory()
        {
            XmlConfigurator.Configure();
        }

        public ILog GetLog(Type t)
        {
            return new Log(
                log4net.LogManager.GetLogger(t)
            );
        }
    }
}
