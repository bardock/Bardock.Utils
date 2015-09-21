namespace Bardock.Utils.Data.EF.Operations
{
    public interface IEntityUpdater
    {
        void Update(DbContextBase dbCtx, object e);
    }
}