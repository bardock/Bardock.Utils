namespace Bardock.Utils.Data.EF.Operations
{
    public interface IEntityDetacher
    {
        void Detach(DbContextBase dbCtx, object e);
    }
}