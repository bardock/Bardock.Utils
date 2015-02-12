using Effort.DataLoaders;

namespace Bardock.Utils.UnitTest.Data.EF.Effort.DataLoaders
{
    public class EntityObjectDataLoader : IDataLoader
    {
        private EntityObjectDataLoaderBindingsBuilder _builder;

        public string Argument { get; set; }

        public EntityObjectDataLoader(EntityObjectDataLoaderBindingsBuilder builder)
        {
            _builder = builder;
        }

        public ITableDataLoaderFactory CreateTableDataLoaderFactory()
        {
            return new EntityObjectDataLoaderFactory(_builder.GetBindings());
        }
    }
}