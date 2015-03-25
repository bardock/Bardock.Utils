using Effort.DataLoaders;
using System.Collections.Generic;

namespace Bardock.Utils.UnitTest.Data.EF.Effort.DataLoaders
{
    internal class EntityObjectDataLoaderFactory : ITableDataLoaderFactory
    {
        private IDictionary<string, IEntityDataLoader<object>> _bindings;

        public EntityObjectDataLoaderFactory(IDictionary<string, IEntityDataLoader<object>> bindings)
        {
            _bindings = bindings;
        }

        public ITableDataLoader CreateTableDataLoader(TableDescription table)
        {
            if (!_bindings.ContainsKey(table.Name))
                return new EmptyTableDataLoader();

            return new EntityObjectDataLoaderWrapper(_bindings[table.Name], table.Columns);
        }

        public void Dispose()
        {
        }
    }
}