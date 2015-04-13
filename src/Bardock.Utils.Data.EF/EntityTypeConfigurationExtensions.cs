using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Bardock.Utils.Extensions;

namespace Bardock.Utils.Data.EF
{
    public static class EntityTypeConfigurationExtensions
    {
        public static EntityTypeConfiguration<TEntity> HasIndex<TEntity>(
            this EntityTypeConfiguration<TEntity> config,
            params Expression<Func<TEntity, object>>[] fields)
            where TEntity : class
        {
            return config.HasIndex(name: null, fields: fields);
        }

        public static EntityTypeConfiguration<TEntity> HasIndex<TEntity>(
            this EntityTypeConfiguration<TEntity> config,
            string name,
            params Expression<Func<TEntity, object>>[] fields)
            where TEntity : class
        {
            return config.HasIndex(name: name, isUnique: false, fields: fields);
        }

        public static EntityTypeConfiguration<TEntity> HasUniqueIndex<TEntity>(
            this EntityTypeConfiguration<TEntity> config,
            params Expression<Func<TEntity, object>>[] fields)
            where TEntity : class
        {
            return config.HasUniqueIndex(name: null, fields: fields);
        }

        public static EntityTypeConfiguration<TEntity> HasUniqueIndex<TEntity>(
            this EntityTypeConfiguration<TEntity> config,
            string name,
            params Expression<Func<TEntity, object>>[] fields)
            where TEntity : class
        {
            return config.HasIndex(name: name, isUnique: true, fields: fields);
        }

        private static EntityTypeConfiguration<TEntity> HasIndex<TEntity>(
            this EntityTypeConfiguration<TEntity> config,
            string name,
            bool isUnique,
            params Expression<Func<TEntity, object>>[] fields)
            where TEntity : class
        {
            var fieldsProperties = fields
                .Select(x => (MemberExpression)(x.Body.RemoveConvert()))
                .Select(x => (PropertyInfo)x.Member)
                .ToList();

            name = name ?? ("IX-" + string.Join("-", fieldsProperties.Select(x => x.Name)));

            var i = 0;
            foreach (var prop in fieldsProperties)
            {
                var propertyMethod = config.GetType().GetMethods()
                    .Where(x => x.Name == "Property")
                    .Select(x => new
                    {
                        Method = x,
                        PropertyType = GetPropertyTypeOfPropertyMethod(x),
                    })
                    .Select(x => new
                    {
                        Method = x.Method,
                        Priority = x.PropertyType == prop.PropertyType ? 1 :
                            // check if required overload is: Property<T>(Expression<Func<TStructuralType, T>> propertyExpression) where T : struct
                            x.PropertyType.IsGenericParameter(nullable: false) && prop.PropertyType.IsValueType(nullable: false) ? 2 :
                            // check if required overload is: Property<T>(Expression<Func<TStructuralType, T?>> propertyExpression) where T : struct
                            x.PropertyType.IsGenericParameter(nullable: true) && prop.PropertyType.IsValueType(nullable: true) ? 3 : 0
                    })
                    .Where(x => x.Priority != 0)
                    .OrderBy(x => x.Priority)
                    .Select(x => x.Method)
                    .First();

                if (propertyMethod.IsGenericMethod)
                    propertyMethod = propertyMethod.MakeGenericMethod(prop.PropertyType.GetNullableUnderlyingType() ?? prop.PropertyType);

                var propertyConfig = propertyMethod.Invoke(config, new[] { prop.ToLambdaExpression() }) as PrimitivePropertyConfiguration;

                propertyConfig
                    .HasColumnAnnotation(
                        "Index",
                        new IndexAnnotation(
                            new IndexAttribute(name, i + 1) { IsUnique = isUnique })); ;
                i++;
            }
            return config;
        }

        /// <summary>
        /// Gets the return type of the Expression at first argument of the specified method.
        /// For example, if propertyMethod is Property(Expression<Func<TStructuralType, byte[]>>)
        /// then the byte[] type is returned.
        /// </summary>
        /// <param name="propertyMethod">A "Property" method of <see cref="StructuralTypeConfiguration<TStructuralType>"/></param>
        /// <returns></returns>
        private static Type GetPropertyTypeOfPropertyMethod(MethodInfo propertyMethod)
        {
            return propertyMethod // ej: Property(Expression<Func<TStructuralType, byte[]>>
                .GetParameters().First().ParameterType // ej: Expression<Func<TStructuralType, byte[]>>
                .GetGenericArguments().First() // ej: Func<TStructuralType, byte[]>
                .GetGenericArguments()[1]; // ej: byte[]
        }
    }
}