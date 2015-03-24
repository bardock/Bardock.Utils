using Effort.DataLoaders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bardock.Utils.UnitTest.Data.EF.Effort.DataLoaders
{
    public class EntityObjectDataLoader : IDataLoader
    {
        private IDictionary<string, IEntityDataLoader<object>> _bindings;

        // TODO FIXME: Effort needs a parameterless constructor
        public static Action<BindingsBuilder> Config { get; set; }

        public EntityObjectDataLoader()
        {
            Init();
        }

        public EntityObjectDataLoader(Action<BindingsBuilder> config)
        {
            Config = config;
            Init();
        }

        private void Init()
        {
            if (Config == null)
                throw new ArgumentNullException("Config");

            var builder = new BindingsBuilder();

            Config(builder);

            _bindings = builder.Build();

            if (_bindings == null || !_bindings.Any())
                throw new NotValidBindingsException("Bindings null or empty");
        }

        public string Argument { get; set; }

        public ITableDataLoaderFactory CreateTableDataLoaderFactory()
        {
            return new EntityObjectDataLoaderFactory(_bindings);
        }

        public class BindingsBuilder
        {
            private IDictionary<string, IEntityDataLoader<object>> _bindings;

            public BindingsBuilder()
            {
                _bindings = new Dictionary<string, IEntityDataLoader<object>>();
            }

            public BindingsBuilder Add<TEntity>(string tableName, IEntityDataLoader<TEntity> loader)
                where TEntity : class
            {
                AddBinding(tableName, loader);
                return this;
            }

            public BindingsBuilder Add<TEntity>(IEntityDataLoader<TEntity> loader)
                where TEntity : class
            {
                AddBinding(typeof(TEntity).Name, loader);
                return this;
            }

            internal IDictionary<string, IEntityDataLoader<object>> Build()
            {
                return _bindings;
            }

            private void AddBinding<TEntity>(string tableName, IEntityDataLoader<TEntity> loader)
                where TEntity : class
            {
                _bindings.Add(tableName, loader);
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