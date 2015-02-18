using Effort.DataLoaders;
using System;
using System.Linq;

namespace Bardock.Utils.UnitTest.Data.EF.Effort.DataLoaders
{
    public class EntityObjectDataLoader : IDataLoader
    {
        private EntityObjectDataLoaderBindingsBuilder _builder;

        public string Argument { get; set; }

        public EntityObjectDataLoader(EntityObjectDataLoaderBindingsBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException("builder");

            _builder = builder;
        }

        public ITableDataLoaderFactory CreateTableDataLoaderFactory()
        {
            var bindings = _builder.GetBindings();

            if (bindings == null || !bindings.Any())
                throw new NotValidBindingsException("Bindings null or empty");

            return new EntityObjectDataLoaderFactory(bindings);
        }

        public class NotValidBindingsException : Exception
        {
            public NotValidBindingsException()
                : base() { }

            public NotValidBindingsException(string message)
                : base(message) { }

            public NotValidBindingsException(string message, Exception innerException)
                : base(message, innerException) { }
        }
    }
}