using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Bardock.Utils.UnitTest.Data.EF
{
    /// <summary>
    /// A wrapper for a <see cref="DbContext"/>
    /// </summary>
    public class DataContextWrapper : IDataContextWrapper
    {
        internal DbContext _db;
        private IList<object> _entries;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataContextWrapper"/> class.
        /// </summary>
        /// <param name="db">The database context.</param>
        public DataContextWrapper(DbContext db)
        {
            _db = db;
            _entries = new List<object>();
        }

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public IDataContextWrapper Add<T>(T entity)
            where T : class
        {
            _db.Entry(entity).State = EntityState.Added;

            if (!_entries.Contains(entity))
                _entries.Add(entity);

            return this;
        }

        /// <summary>
        /// Adds all specified <typeparamref name="T" /> to the context.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities">The entities.</param>
        /// <returns>
        /// An <see cref="IDataContextWrapper" />
        /// </returns>
        public IDataContextWrapper AddAll<T>(IEnumerable<T> entities)
            where T : class
        {
            foreach (var i in entities)
                this.Add(i);

            return this;
        }

        /// <summary>
        /// Deletes the specified <typeparamref name="T" /> entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// An <see cref="IDataContextWrapper" />
        /// </returns>
        public IDataContextWrapper Delete<T>(T entity)
            where T : class
        {
            _db.Entry(entity).State = EntityState.Deleted;

            if (!_entries.Contains(entity))
                _entries.Add(entity);

            return this;
        }

        /// <summary>
        /// Deletes all specified <typeparamref name="T" /> entities.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities">The entities.</param>
        /// <returns>
        /// An <see cref="IDataContextWrapper" />
        /// </returns>
        public IDataContextWrapper DeleteAll<T>(IEnumerable<T> entities)
            where T : class
        {
            foreach (var i in entities)
                this.Delete(i);

            return this;
        }

        /// <summary>
        /// Gets a query for <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>
        /// A query for a specified <typeparamref name="T" />.
        /// </returns>
        public IQueryable<T> GetQuery<T>()
            where T : class
        {
            return _db.Set<T>().AsQueryable();
        }

        /// <summary>
        /// This method applies all the changes to the database and
        /// detachs all related entities.
        /// </summary>
        /// <returns>
        /// An <see cref="IDataContextWrapper" />
        /// </returns>
        public IDataContextWrapper Save()
        {
            _db.SaveChanges();

            this.DetachAll(_entries.ToList());

            return this;
        }

        /// <summary>
        /// This method applies all the changes to the database asynchronously
        /// and detachs all related entities.
        /// </summary>
        /// <returns>
        /// An <see cref="IDataContextWrapper" />
        /// </returns>
        public async Task<IDataContextWrapper> SaveAsync()
        {
            await _db.SaveChangesAsync();
            return this;
        }

        /// <summary>
        /// Updates the specified <typeparamref name="T" /> entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// An <see cref="IDataContextWrapper" />
        /// </returns>
        public IDataContextWrapper Update<T>(T entity)
            where T : class
        {
            _db.Entry(entity).State = EntityState.Modified;

            if (!_entries.Contains(entity))
                _entries.Add(entity);

            return this;
        }

        /// <summary>
        /// Updates all specified <typeparamref name="T" /> entities.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities">The entities.</param>
        /// <returns>
        /// An <see cref="IDataContextWrapper" />
        /// </returns>
        public IDataContextWrapper UpdateAll<T>(IEnumerable<T> entities)
            where T : class
        {
            foreach (var i in entities)
                this.Update(i);

            return this;
        }

        /// <summary>
        /// Detaches the specified entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        private DataContextWrapper Detach<T>(T entity)
            where T : class
        {
            _db.Entry(entity).State = EntityState.Detached;

            _entries.Remove(entity);

            return this;
        }

        /// <summary>
        /// Detaches all.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        private DataContextWrapper DetachAll<T>(IEnumerable<T> entities)
            where T : class
        {
            foreach (var i in entities)
                this.Detach(i);

            return this;
        }
    }
}