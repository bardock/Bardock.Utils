using System.Data.Entity;

namespace Bardock.Utils.Data.EF.Operations
{
    public class EntityDeleter : IEntityDeleter
    {
        public void Delete(DbContextBase dbCtx, object e)
        {
            dbCtx.Entry(e).State = EntityState.Deleted;
        }
    }
}