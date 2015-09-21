namespace Bardock.Utils.Data.EF.Operations
{
    public interface IEntityAdder
    {
        void Add(DbContextBase dbCtx, object e);
    }
}