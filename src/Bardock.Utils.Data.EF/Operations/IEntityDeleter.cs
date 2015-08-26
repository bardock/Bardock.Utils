namespace Bardock.Utils.Data.EF.Operations
{
    public interface IEntityDeleter
    {
        void Delete(DbContextBase dbCtx, object e);
    }
}