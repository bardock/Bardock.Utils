using System.Data.Entity;

namespace Bardock.Utils.Data.EF.Operations
{
    public class EntityAdder : IEntityAdder
    {
        public void Add(DbContextBase dbCtx, object e)
        {
            if (dbCtx.Entry(e).State != EntityState.Modified)
            {
                dbCtx.Entry(e).State = EntityState.Added;
            }
        }
    }
}