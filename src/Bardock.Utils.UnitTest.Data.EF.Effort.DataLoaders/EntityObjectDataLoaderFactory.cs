using Effort.DataLoaders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bardock.Utils.UnitTest.Data.EF.Effort.DataLoaders
{
    internal class EntityObjectDataLoaderFactory : ITableDataLoaderFactory
    {
        private IDictionary<string, string> _bindings;

        public EntityObjectDataLoaderFactory(IDictionary<string, string> bindings)
        {
            _bindings = bindings;
        }

        public ITableDataLoader CreateTableDataLoader(TableDescription table)
        {
            if (!_bindings.ContainsKey(table.Name))
                return new EmptyTableDataLoader();

            var entry = _bindings.Single(x => x.Key == table.Name);

            return new EntityObjectDataLoaderWrapper(
                (IEntityDataLoader<object>)Activator.CreateInstance(Type.GetType(entry.Value, throwOnError: false)),
                table.Columns
            );
        }

        public void Dispose()
        {
        }
    }
}