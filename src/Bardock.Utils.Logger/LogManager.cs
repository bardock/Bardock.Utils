using System;
using System.Collections.Generic;
using System.Reflection;
using Bardock.Utils.Types;

namespace Bardock.Utils.Logger
{
    /// <summary>
    /// Manages the ILog instances
    /// </summary>
    public class LogManager
    {
        private Dictionary<string, ILog> _logs = new Dictionary<string, ILog>();
        private object _syncLock = new object();        

        private static LogManager _default = new LogManager();

        /// <summary>
        /// Get the default <see cref="LogManager" /> instance.
        /// </summary>
        public static LogManager Default { get { return _default; } }


        private ILogFactory _factory = null;

        /// <summary>
        /// Get or set ILogFactory implementation used to create the ILog instances.
        /// Using this property you can inject the desired ILogFactory.
        /// If there is defined the implementation in config, it will be used by default.
        /// Otherwise a <see cref="NullLogFactory"/> is used.
        /// </summary>
        public ILogFactory Factory
        {
            get
            {
                if (_factory == null)
                {
                    if (ConfigSection.Default == null || String.IsNullOrEmpty(ConfigSection.Default.LogFactory))
                    {
                        _factory = new NullLogFactory();
                    }
                    else
                    {
                        _factory = (ILogFactory)TypeActivator.CreateFromTypeName(ConfigSection.Default.LogFactory);
                    }
                }
                return _factory;
            }
            set 
            {
                _factory = value;
            }
        }

        public ILog GetLog<T>()
        {
            return GetLog(typeof(T));
        }

        public ILog GetLog(object o)
        {
            return GetLog(o.GetType());
        }

        public ILog GetLog(Type t)
        {
            return GetLog(t.FullName);
        }

        public ILog GetLog(string name)
        {
            if (!_logs.ContainsKey(name))
            {
                lock (_syncLock)
                {
                    if (!_logs.ContainsKey(name))
                    {
                        _logs[name] = Factory.GetLog(name);
                    }
                }
            }
            return _logs[name];
        }

    }
}