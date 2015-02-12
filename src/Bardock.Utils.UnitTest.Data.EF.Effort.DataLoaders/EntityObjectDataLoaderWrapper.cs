using Effort.DataLoaders;
using System.Collections.Generic;
using System.Linq;

namespace Bardock.Utils.UnitTest.Data.EF.Effort.DataLoaders
{
    internal class EntityObjectDataLoaderWrapper : ITableDataLoader
    {
        private IEntityDataLoader<object> _loader;
        private IReadOnlyCollection<ColumnDescription> _columns;

        public EntityObjectDataLoaderWrapper(IEntityDataLoader<object> loader, IReadOnlyCollection<ColumnDescription> columns)
        {
            _loader = loader;
            _columns = columns;
        }

        public IEnumerable<object[]> GetData()
        {
            return _loader
                    .GetData()
                    .Select(obj => _columns
                                    .Select(c => obj.GetType()
                                                    .GetProperty(c.Name)
                                                    .GetValue(obj))
                                    .ToArray());
        }
    }
}