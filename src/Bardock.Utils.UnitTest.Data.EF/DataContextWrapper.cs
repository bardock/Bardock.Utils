using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Bardock.Utils.UnitTest.Data.EntityFramework
{
    public class DataContextWrapper : IDataContextWrapper
    {
        internal DbContext _db;

        public DataContextWrapper(DbContext db)
        {
            _db = db;
        }

        public IQueryable<T> GetQuery<T>()
            where T : class
        {
            return _db.Set<T>().AsQueryable();
        }

        public IDataContextWrapper Add<T>(T e)
            where T : class
        {
            _db.Entry(e).State = EntityState.Added;
            return this;
        }

        public IDataContextWrapper Add<T>(IEnumerable<T> e)
            where T : class
        {
            foreach (var i in e)
                this.Add(i);

            return this;
        }

        public IDataContextWrapper Update<T>(T e)
            where T : class
        {
            _db.Entry(e).State = EntityState.Modified;
            return this;
        }

        public IDataContextWrapper Update<T>(IEnumerable<T> e)
            where T : class
        {
            foreach (var i in e)
                this.Update(i);

            return this;
        }

        public IDataContextWrapper Delete<T>(T e)
            where T : class
        {
            _db.Entry(e).State = EntityState.Deleted;
            return this;
        }

        public IDataContextWrapper Delete<T>(IEnumerable<T> e)
            where T : class
        {
            foreach (var i in e)
                this.Delete(i);

            return this;
        }

        public IDataContextWrapper Detach<T>(T e)
            where T : class
        {
            _db.Entry(e).State = EntityState.Detached;
            return this;
        }

        public IDataContextWrapper Detach<T>(IEnumerable<T> e)
            where T : class
        {
            foreach (var i in e)
                this.Detach(i);

            return this;
        }

        public IDataContextWrapper Save()
        {
            _db.SaveChanges();
            return this;
        }

        public async Task<IDataContextWrapper> SaveAsync()
        {
            await _db.SaveChangesAsync();
            return this;
        }
    }
}