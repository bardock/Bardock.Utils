using Bardock.Utils.Scoping;
using Bardock.Utils.UnitTest.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Bardock.Utils.UnitTest.Data.EF
{
    public class DataContextScope : Scope<DbContext>
    {
        private DataContextWrapper _wrapper;
        private IList<object> _entries;

        public DataContextScope(DataContextWrapper wrapper, Func<Builder, Builder> factoryFunc)
            : base(wrapper._db, factoryFunc)
        {
            _wrapper = wrapper;
            _entries = new List<object>();
        }

        public DataContextScope Add<T>(T e)
            where T : class
        {
            if (!_entries.Contains(e))
                _entries.Add(e);

            _wrapper.Add(e);
            return this;
        }

        public DataContextScope Add<T>(IEnumerable<T> e)
            where T : class
        {
            foreach (var i in e)
                if (!_entries.Contains(i))
                    _entries.Add(i);

            _wrapper.Add(e);
            return this;
        }

        public DataContextScope Update<T>(T e)
            where T : class
        {
            if (!_entries.Contains(e))
                _entries.Add(e);

            _wrapper.Update(e);
            return this;
        }

        public DataContextScope Update<T>(IEnumerable<T> e)
            where T : class
        {
            foreach (var i in e)
                if (!_entries.Contains(i))
                    _entries.Add(i);

            _wrapper.Update(e);
            return this;
        }

        public DataContextScope Delete<T>(T e)
            where T : class
        {
            if (!_entries.Contains(e))
                _entries.Add(e);

            _wrapper.Delete(e);
            return this;
        }

        public DataContextScope Delete<T>(IEnumerable<T> e)
            where T : class
        {
            foreach (var i in e)
                if (!_entries.Contains(i))
                    _entries.Add(i);

            _wrapper.Delete(e);
            return this;
        }

        public override void Dispose()
        {
            base.Dispose();

            _wrapper
                .Save()
                .Detach(_entries);
        }
    }
}