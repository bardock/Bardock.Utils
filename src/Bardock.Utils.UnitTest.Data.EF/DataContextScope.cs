using Bardock.Utils.Scoping;
using System;
using System.Data.Entity;

namespace Bardock.Utils.UnitTest.Data.EF
{
    public class DataContextScope : Scope<DbContext>, IDataContextScope
    {
        public DataContextScope(DataContextWrapper wrapper, Action<Builder> factoryFunc)
            : base(wrapper._db, factoryFunc)
        {
            Db = wrapper;
        }

        public IDataContextWrapper Db { get; private set; }

        public override void Dispose()
        {
            Db.Save();

            base.Dispose();
        }
    }
}