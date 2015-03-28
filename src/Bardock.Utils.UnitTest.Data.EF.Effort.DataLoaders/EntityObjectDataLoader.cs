using Effort.DataLoaders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bardock.Utils.UnitTest.Data.EF.Effort.DataLoaders
{
    public class EntityObjectDataLoader : IDataLoader
    {
        private IDictionary<string, string> _bindings;

        public EntityObjectDataLoader()
        {
        }

        public EntityObjectDataLoader(Action<BindingsBuilder> config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            var builder = new BindingsBuilder();

            config(builder);

            _bindings = builder.Build();

            if (_bindings == null || !_bindings.Any())
                throw new NotValidBindingsException("Bindings null or empty");
        }

        public string Argument
        {
            get { return Newtonsoft.Json.JsonConvert.SerializeObject(_bindings); }
            set { _bindings = Newtonsoft.Json.JsonConvert.DeserializeObject<IDictionary<string, string>>(value); }
        }

        public ITableDataLoaderFactory CreateTableDataLoaderFactory()
        {
            return new EntityObjectDataLoaderFactory(_bindings);
        }

        public class BindingsBuilder
        {
            private IDictionary<string, string> _bindings;

            public BindingsBuilder()
            {
                _bindings = new Dictionary<string, string>();
            }

            public BindingsBuilder Add<TEntityObjectDataLoader>()
                where TEntityObjectDataLoader : IEntityDataLoader<object>, new()
            {
                return this.AddBinding(typeof(TEntityObjectDataLoader));
            }

            public BindingsBuilder Add<TEntityObjectDataLoader>(string tableName)
            {
                return this.AddBinding(typeof(TEntityObjectDataLoader), tableName);
            }

            internal IDictionary<string, string> Build()
            {
                return _bindings.ToDictionary(e => e.Key, e => e.Value);
            }

            private BindingsBuilder AddBinding(Type type, string tableName = null)
            {
                _bindings.Add(
                    tableName ?? type.Name.Replace("DataLoader", string.Empty),
                    string.Format("{0}, {1}", type.FullName, type.Assembly.FullName));

                return this;
            }
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