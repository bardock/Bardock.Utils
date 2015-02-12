using System.Collections.Generic;

namespace Bardock.Utils.UnitTest.Data.EF.Effort.DataLoaders
{
    public class EntityObjectDataLoaderBindingsBuilder
    {
        private IDictionary<string, IEntityDataLoader<object>> _bindings;

        public EntityObjectDataLoaderBindingsBuilder()
        {
            _bindings = new Dictionary<string, IEntityDataLoader<object>>();
        }

        public EntityObjectDataLoaderBindingsBuilder Add<TEntity>(string tableName, IEntityDataLoader<TEntity> loader)
            where TEntity : class
        {
            AddBinding(tableName, loader);
            return this;
        }

        public EntityObjectDataLoaderBindingsBuilder Add<TEntity>(IEntityDataLoader<TEntity> loader)
            where TEntity : class
        {
            AddBinding(typeof(TEntity).Name, loader);
            return this;
        }

        public IDictionary<string, IEntityDataLoader<object>> GetBindings()
        {
            return this._bindings;
        }

        private void AddBinding<TEntity>(string tableName, IEntityDataLoader<TEntity> loader)
            where TEntity : class
        {
            _bindings.Add(tableName, loader);
        }
    }
}