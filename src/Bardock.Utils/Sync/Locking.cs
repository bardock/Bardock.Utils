using System.Collections.Generic;

namespace Bardock.Utils.Sync
{
    public class Locking
    {
        private Dictionary<object, object> Instances = new Dictionary<object, object>();

        public object GetLock(object param)
        {
            if (!Instances.ContainsKey(param))
                Instances[param] = new object();
            return Instances[param];
        }
    }
}
