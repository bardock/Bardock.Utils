using System.Collections.Generic;

namespace Bardock.Utils.UnitTest.Data.EF.Effort.DataLoaders
{
    /// <summary>
    /// Represents a typed data loader.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IEntityDataLoader<out TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Gets the collection of entities.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> GetData();
    }
}