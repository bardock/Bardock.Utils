using System.Configuration;
using System.Runtime.Serialization;

namespace Bardock.Utils.Logger
{
    public class ConfigSection : ConfigurationSection
    {
        public static ConfigSection Default
        {
            get { return (ConfigSection)ConfigurationManager.GetSection("Bardock.Utils/Logger"); }
        }

        [ConfigurationProperty("LogFactory", IsRequired = false)]
        public string LogFactory
        {
            get { return (string)this["LogFactory"]; }
            set { this["LogFactory"] = value; }
        }
    }
}