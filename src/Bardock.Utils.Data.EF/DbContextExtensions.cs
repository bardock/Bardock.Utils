using System.Collections.Generic;

namespace Bardock.Utils.Data.EF
{
    public static class DbContextExtensions
    {
        public static TDbContext Delete<TDbContext>(this TDbContext db, object e)
            where TDbContext : DbContextBase
        {
            db.Delete(e);
            return db;
        }

        public static TDbContext Delete<TDbContext, T>(this TDbContext db, IEnumerable<T> e)
            where TDbContext : DbContextBase
        {
            foreach (var item in e)
            {
                db.Delete(item);
            }
            return db;
        }

        public static TDbContext Add<TDbContext>(this TDbContext db, object e)
            where TDbContext : DbContextBase
        {
            db.Add(e);
            return db;
        }

        public static TDbContext Add<TDbContext, T>(this TDbContext db, IEnumerable<T> e)
            where TDbContext : DbContextBase
        {
            foreach (var item in e)
            {
                db.Add(item);
            }
            return db;
        }

        public static TDbContext Update<TDbContext>(this TDbContext db, object e)
            where TDbContext : DbContextBase
        {
            db.Update(e);
            return db;
        }

        public static TDbContext Update<TDbContext, T>(this TDbContext db, IEnumerable<T> e)
            where TDbContext : DbContextBase
        {
            foreach (var item in e)
            {
                db.Update(item);
            }
            return db;
        }

        public static TDbContext Detach<TDbContext>(this TDbContext db, object e)
            where TDbContext : DbContextBase
        {
            db.Detach(e);
            return db;
        }

        public static TDbContext Detach<TDbContext, T>(this TDbContext db, IEnumerable<T> e)
            where TDbContext : DbContextBase
        {
            foreach (var item in e)
            {
                db.Detach(item);
            }
            return db;
        }
    }
}