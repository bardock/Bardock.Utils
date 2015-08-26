using System.Data.Entity;

namespace Bardock.Utils.Data.EF.Operations
{
    public class EntityUpdater : IEntityUpdater
    {
        public void Update(DbContextBase dbCtx, object e)
        {
            if (dbCtx.Entry(e).State != EntityState.Modified)
            {
                dbCtx.Entry(e).State = EntityState.Modified;
            }
        }
    }
}