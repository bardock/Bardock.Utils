using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bardock.Utils.UnitTest.Data
{
    /// <summary>
    /// A wrapper for a DataContext
    /// </summary>
    public interface IDataContextWrapper
    {
        /// <summary>
        /// Gets a query for <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>
        /// A query for a specified <typeparamref name="T"/>.
        /// </returns>
        IQueryable<T> GetQuery<T>()
            where T : class;

        /// <summary>
        /// Adds specified <typeparamref name="T"/> to the context.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity</param>
        /// <returns>
        /// An <see cref="IDataContextWrapper"/>
        /// </returns>
        IDataContextWrapper Add<T>(T entity)
            where T : class;

        /// <summary>
        /// Adds all specified <typeparamref name="T"/> to the context.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities">The entities.</param>
        /// <returns>
        /// An <see cref="IDataContextWrapper"/>
        /// </returns>
        IDataContextWrapper AddAll<T>(IEnumerable<T> entities)
            where T : class;

        /// <summary>
        /// Updates the specified <typeparamref name="T"/> entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// An <see cref="IDataContextWrapper"/>
        /// </returns>
        IDataContextWrapper Update<T>(T entity)
            where T : class;

        /// <summary>
        /// Updates all specified <typeparamref name="T"/> entities.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities">The entities.</param>
        /// <returns>
        /// An <see cref="IDataContextWrapper"/>
        /// </returns>
        IDataContextWrapper UpdateAll<T>(IEnumerable<T> entities)
            where T : class;

        /// <summary>
        /// Deletes the specified <typeparamref name="T"/> entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// An <see cref="IDataContextWrapper"/>
        /// </returns>
        IDataContextWrapper Delete<T>(T entity)
            where T : class;

        /// <summary>
        /// Deletes all specified <typeparamref name="T"/> entities.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities">The entities.</param>
        /// <returns>
        /// An <see cref="IDataContextWrapper"/>
        /// </returns>
        IDataContextWrapper DeleteAll<T>(IEnumerable<T> entities)
            where T : class;

        /// <summary>
        /// This method applies all the changes to the database and
        /// detachs all related entities.
        /// </summary>
        /// <returns>
        /// An <see cref="IDataContextWrapper"/>
        /// </returns>
        IDataContextWrapper Save();

        /// <summary>
        /// This method applies all the changes to the database asynchronously
        /// and detachs all related entities.
        /// </summary>
        /// <returns>
        /// An <see cref="IDataContextWrapper"/>
        /// </returns>
        Task<IDataContextWrapper> SaveAsync();
    }
}