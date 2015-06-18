using Effort.DataLoaders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bardock.Utils.UnitTest.Data.EF.Effort.DataLoaders
{
    /// <summary>
    /// A typed Effort data loader.
    /// </summary>
    public class EntityObjectDataLoader : IDataLoader
    {
        private IDictionary<string, string> _bindings;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityObjectDataLoader"/> class.
        /// </summary>
        public EntityObjectDataLoader()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityObjectDataLoader"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <exception cref="System.ArgumentNullException">config</exception>
        /// <exception cref="Bardock.Utils.UnitTest.Data.EF.Effort.DataLoaders.EntityObjectDataLoader.NotValidBindingsException">Bindings null or empty</exception>
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

        /// <summary>
        /// Gets or sets the argument that describes the complete state of the data loader.
        /// </summary>
        /// <value>
        /// The argument.
        /// </value>
        public string Argument
        {
            get { return Newtonsoft.Json.JsonConvert.SerializeObject(_bindings); }
            set { _bindings = Newtonsoft.Json.JsonConvert.DeserializeObject<IDictionary<string, string>>(value); }
        }

        /// <summary>
        /// Creates a <see cref="EntityObjectDataLoaderFactory"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="EntityObjectDataLoaderFactory"/>.
        /// </returns>
        public ITableDataLoaderFactory CreateTableDataLoaderFactory()
        {
            return new EntityObjectDataLoaderFactory(_bindings);
        }

        /// <summary>
        /// A builder that binds entities to data loaders.
        /// </summary>
        public class BindingsBuilder
        {
            private IDictionary<string, string> _bindings;

            /// <summary>
            /// Initializes a new instance of the <see cref="BindingsBuilder"/> class.
            /// </summary>
            public BindingsBuilder()
            {
                _bindings = new Dictionary<string, string>();
            }

            /// <summary>
            /// Adds an <see cref="IEntityDataLoader"/> instance.
            /// </summary>
            /// <typeparam name="TEntityObjectDataLoader">The type of the entity object data loader.</typeparam>
            /// <returns>
            /// The <see cref="BindingsBuilder"/>
            /// </returns>
            public BindingsBuilder Add<TEntityObjectDataLoader>()
                where TEntityObjectDataLoader : IEntityDataLoader<object>, new()
            {
                return this.AddBinding(typeof(TEntityObjectDataLoader));
            }

            /// <summary>
            /// Adds an <see cref="IEntityDataLoader"/> instance binded to the specified table.
            /// </summary>
            /// <typeparam name="TEntityObjectDataLoader">The type of the entity object data loader.</typeparam>
            /// <param name="tableName">Name of the table.</param>
            /// <returns>
            /// The <see cref="BindingsBuilder"/>
            /// </returns>
            public BindingsBuilder Add<TEntityObjectDataLoader>(string tableName)
            {
                return this.AddBinding(typeof(TEntityObjectDataLoader), tableName);
            }

            /// <summary>
            /// Builds a dictionary containing binded data loaders.
            /// </summary>
            /// <returns>
            /// A dictionary containing binded data loaders
            /// </returns>
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

        /// <summary>
        /// Indicates that bindings are null or empty
        /// </summary>
        public class NotValidBindingsException : Exception
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="NotValidBindingsException"/> class.
            /// </summary>
            /// <param name="message">The message that describes the error.</param>
            public NotValidBindingsException(string message)
                : base(message) { }
        }
    }
}