using System;
using System.Collections.Generic;
using System.Reflection;
namespace Bardock.Utils.Logger
{
    public static class Manager
    {
        private static Dictionary<Type, ILog> _logs = new Dictionary<Type, ILog>();

        private static ILogFactory _factory = null;

        private static ILogFactory Factory
        {
            get
            {
                if (_factory == null)
                {
                    if (ConfigSection.Default == null || String.IsNullOrEmpty(ConfigSection.Default.LogFactory))
                    {
                        _factory = new LogFactoryMock();
                    }
                    else
                    {
                        string[] assemblyAndTypeNames = ConfigSection.Default.LogFactory.Split(new char[] { ',' });
                        var assembly = Assembly.Load(assemblyAndTypeNames[1].Trim());
                        var type = assembly.GetType(assemblyAndTypeNames[0].Trim());
                        _factory = (ILogFactory)Activator.CreateInstance(type);
                    }
                }
                return _factory;
            }
        }

        public static ILog GetLog<T>()
        {
            return GetLog(typeof(T));
        }

        public static ILog GetLog(object o)
        {
            return GetLog(o.GetType());
        }

        public static ILog GetLog(Type t)
        {
            if ((!_logs.ContainsKey(t)))
            {
                _logs[t] = Factory.GetLog(t);
            }
            return _logs[t];
        }

    }
}