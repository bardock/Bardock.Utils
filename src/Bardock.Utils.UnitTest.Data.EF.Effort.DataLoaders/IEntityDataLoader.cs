using System.Collections.Generic;

namespace Bardock.Utils.UnitTest.Data.EF.Effort.DataLoaders
{
    public interface IEntityDataLoader<out TEntity>
        where TEntity : class
    {
        IEnumerable<TEntity> GetData();
    }
}