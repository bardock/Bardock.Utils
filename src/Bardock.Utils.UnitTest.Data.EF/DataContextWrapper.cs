using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Bardock.Utils.UnitTest.Data.EF
{
    public class DataContextWrapper : IDataContextWrapper
    {
        internal DbContext _db;
        private IList<object> _entries;

        public DataContextWrapper(DbContext db)
        {
            _db = db;
            _entries = new List<object>();
        }

        public IDataContextWrapper Add<T>(T e)
            where T : class
        {
            _db.Entry(e).State = EntityState.Added;

            if (!_entries.Contains(e))
                _entries.Add(e);

            return this;
        }

        public IDataContextWrapper AddAll<T>(IEnumerable<T> e)
            where T : class
        {
            foreach (var i in e)
                this.Add(i);

            return this;
        }

        public IDataContextWrapper Delete<T>(T e)
            where T : class
        {
            _db.Entry(e).State = EntityState.Deleted;

            if (!_entries.Contains(e))
                _entries.Add(e);

            return this;
        }

        public IDataContextWrapper DeleteAll<T>(IEnumerable<T> e)
            where T : class
        {
            foreach (var i in e)
                this.Delete(i);

            return this;
        }

        public IQueryable<T> GetQuery<T>()
            where T : class
        {
            return _db.Set<T>().AsQueryable();
        }

        public IDataContextWrapper Save()
        {
            _db.SaveChanges();

            this.DetachAll(_entries.ToList());

            return this;
        }

        public async Task<IDataContextWrapper> SaveAsync()
        {
            await _db.SaveChangesAsync();
            return this;
        }

        public IDataContextWrapper Update<T>(T e)
            where T : class
        {
            _db.Entry(e).State = EntityState.Modified;

            if (!_entries.Contains(e))
                _entries.Add(e);

            return this;
        }

        public IDataContextWrapper UpdateAll<T>(IEnumerable<T> e)
            where T : class
        {
            foreach (var i in e)
                this.Update(i);

            return this;
        }

        private DataContextWrapper Detach<T>(T e)
            where T : class
        {
            _db.Entry(e).State = EntityState.Detached;

            _entries.Remove(e);

            return this;
        }

        private DataContextWrapper DetachAll<T>(IEnumerable<T> e)
            where T : class
        {
            foreach (var i in e)
                this.Detach(i);

            return this;
        }
    }
}