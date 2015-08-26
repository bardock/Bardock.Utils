using System.Data.Entity;

namespace Bardock.Utils.Data.EF.Operations
{
    public class EntityDetacher : IEntityDetacher
    {
        public void Detach(DbContextBase dbCtx, object e)
        {
            dbCtx.Entry(e).State = EntityState.Detached;
        }
    }
}