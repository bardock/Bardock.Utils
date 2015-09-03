using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using Bardock.Utils.Extensions;

namespace Bardock.Utils.Data.EF
{
    public static class DbModelBuilderExtensions
    {
        private readonly static MethodInfo _EntityMethod = typeof(DbModelBuilder).GetMethod("Entity");

        /// <summary>
        /// Registers an entity type as part of the model and returns an object that
        /// can be used to configure the entity. This method can be called multiple times
        /// for the same entity to perform multiple lines of configuration.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="entityType">The type to be registered or configured.</param>
        /// <returns>The configuration object for the specified entity type.</returns>
        public static EntityTypeConfiguration Entity(this DbModelBuilder @this, Type entityType)
        {
            var conf = _EntityMethod.MakeGenericMethod(entityType).Invoke(@this, null);
            return new EntityTypeConfiguration(conf);
        }
    }

    /// <summary>
    /// This is a wrapper of <see cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<>"/>
    /// that hides the entity type generic parameter.
    /// Allows configuration to be performed for an entity type in a model.  An EntityTypeConfiguration
    /// can be obtained via the Entity method on System.Data.Entity.DbModelBuilder
    /// or a custom type derived from EntityTypeConfiguration can be registered via
    /// the Configurations property on System.Data.Entity.DbModelBuilder.
    /// </summary>
    public class EntityTypeConfiguration
    {
        private readonly object _typedConfig;

        private readonly static Type _typedConfigGenericType = typeof(EntityTypeConfiguration<>);

        private readonly static MethodInfo _ToTableWithSchemaMethod = _typedConfigGenericType
            .GetMethods().Single(x => x.Name == "ToTable" && x.GetParameters().Count() == 2);

        public EntityTypeConfiguration(object typedConfig)
        {
            if (typedConfig == null)
                throw new ArgumentNullException("typedConfig");

            if (!typedConfig.GetType().IsAssignableToGenericType(_typedConfigGenericType))
                throw new ArgumentException("Must be of type " + _typedConfigGenericType.FullName, "typedConfig");

            this._typedConfig = typedConfig;
        }

        /// <summary>
        /// The entity type being configured
        /// </summary>
        public Type EntityType
        {
            get { return _typedConfig.GetType().GetGenericArguments().First(); }
        }

        private MethodInfo GetToTableMethod(bool withSchema = false)
        {
            var paramsCount = withSchema ? 2 : 1;

            return _typedConfig.GetType()
                .GetMethods()
                .Single(x => x.Name == "ToTable" && x.GetParameters().Count() == paramsCount);
        }

        /// <summary>
        /// Configures the table name that this entity type is mapped to.
        /// </summary>
        /// <param name="tableName">The name of the table.</param>
        /// <returns>The same EntityTypeConfiguration instance so that multiple calls can be chained.</returns>
        public EntityTypeConfiguration ToTable(string tableName)
        {
            var conf = GetToTableMethod()
                .Invoke(_typedConfig, new[] { tableName });

            return new EntityTypeConfiguration(conf);
        }

        /// <summary>
        /// Configures the table name that this entity type is mapped to.
        /// </summary>
        /// <param name="tableName">The name of the table.</param>
        /// <param name="schemaName">The database schema of the table.</param>
        /// <returns>The same EntityTypeConfiguration instance so that multiple calls can be chained.</returns>
        public EntityTypeConfiguration ToTable(string tableName, string schemaName)
        {
            var conf = GetToTableMethod(withSchema: true)
                .Invoke(_typedConfig, new[] { tableName, schemaName });

            return new EntityTypeConfiguration(conf);
        }
    }
}