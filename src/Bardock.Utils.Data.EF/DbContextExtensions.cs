using System.Collections.Generic;
using System.Data.Entity;

namespace Bardock.Utils.Data.EF
{
    public static class DbContextExtensions
    {
        public static TDbContext Delete<TDbContext>(this TDbContext db, object e)
            where TDbContext : DbContext
        {
            db.Entry(e).State = EntityState.Deleted;
            return db;
        }

        public static TDbContext Delete<TDbContext, T>(this TDbContext db, IEnumerable<T> e)
            where TDbContext : DbContext
        {
            foreach (var item in e)
            {
                db.Delete(item);
            }
            return db;
        }

        public static TDbContext Add<TDbContext>(this TDbContext db, object e)
            where TDbContext : DbContext
        {
            if (db.Entry(e).State != EntityState.Modified)
            {
                db.Entry(e).State = EntityState.Added;
            }
            return db;
        }

        public static TDbContext Add<TDbContext, T>(this TDbContext db, IEnumerable<T> e)
            where TDbContext : DbContext
        {
            foreach (var item in e)
            {
                db.Add(item);
            }
            return db;
        }

        public static TDbContext Update<TDbContext>(this TDbContext db, object e)
            where TDbContext : DbContext
        {
            if (db.Entry(e).State != EntityState.Modified)
            {
                db.Entry(e).State = EntityState.Modified;
            }
            return db;
        }

        public static TDbContext Update<TDbContext, T>(this TDbContext db, IEnumerable<T> e)
            where TDbContext : DbContext
        {
            foreach (var item in e)
            {
                db.Update(item);
            }
            return db;
        }

        public static TDbContext Detach<TDbContext>(this TDbContext db, object e)
            where TDbContext : DbContext
        {
            db.Entry(e).State = EntityState.Detached;
            return db;
        }

        public static TDbContext Detach<TDbContext, T>(this TDbContext db, IEnumerable<T> e)
            where TDbContext : DbContext
        {
            foreach (var item in e)
            {
                db.Detach(item);
            }
            return db;
        }
    }
}
