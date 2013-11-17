using System.Collections.Generic;

namespace Bardock.Utils.Sync
{
    public class Lockers
    {
        private Dictionary<object, object> _instances = new Dictionary<object, object>();

        public object GetInstance(object param)
        {
            if (!_instances.ContainsKey(param))
                _instances[param] = new object();
            return _instances[param];
        }
    }
}
