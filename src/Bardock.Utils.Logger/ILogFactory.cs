using System;

namespace Bardock.Utils.Logger
{
    public interface ILogFactory
    {
        ILog GetLog(string name);
    }
}
