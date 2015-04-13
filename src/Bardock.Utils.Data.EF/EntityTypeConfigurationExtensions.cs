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
        public static EntityTypeConfiguration<TEntity> HasIndex<TEntity, TProp>(
            this EntityTypeConfiguration<TEntity> config,
            Expression<Func<TEntity, TProp>> field,
            string name = null,
            bool isUnique = false)
            where TEntity : class
        {
            return config.HasIndex(
                fieldsExpressions: new LambdaExpression[] { field },
                name: name,
                isUnique: isUnique);
        }

        public static EntityTypeConfiguration<TEntity> HasIndex<TEntity, TProp1, TProp2>(
            this EntityTypeConfiguration<TEntity> config,
            Expression<Func<TEntity, TProp1>> field1,
            Expression<Func<TEntity, TProp2>> field2,
            string name = null,
            bool isUnique = false)
            where TEntity : class
        {
            return config.HasIndex(
                fieldsExpressions: new LambdaExpression[] { field1, field2 },
                name: name,
                isUnique: isUnique);
        }

        public static EntityTypeConfiguration<TEntity> HasIndex<TEntity, TProp1, TProp2, TProp3>(
            this EntityTypeConfiguration<TEntity> config,
            Expression<Func<TEntity, TProp1>> field1,
            Expression<Func<TEntity, TProp2>> field2,
            Expression<Func<TEntity, TProp3>> field3,
            string name = null,
            bool isUnique = false)
            where TEntity : class
        {
            return config.HasIndex(
                fieldsExpressions: new LambdaExpression[] { field1, field2, field3 },
                name: name,
                isUnique: isUnique);
        }

        public static EntityTypeConfiguration<TEntity> HasIndex<TEntity, TProp1, TProp2, TProp3, TProp4>(
            this EntityTypeConfiguration<TEntity> config,
            Expression<Func<TEntity, TProp1>> field1,
            Expression<Func<TEntity, TProp2>> field2,
            Expression<Func<TEntity, TProp3>> field3,
            Expression<Func<TEntity, TProp4>> field4,
            string name = null,
            bool isUnique = false)
            where TEntity : class
        {
            return config.HasIndex(
                fieldsExpressions: new LambdaExpression[] { field1, field2, field3, field4 },
                name: name,
                isUnique: isUnique);
        }

        public static EntityTypeConfiguration<TEntity> HasIndex<TEntity, TProp1, TProp2, TProp3, TProp4, TProp5>(
            this EntityTypeConfiguration<TEntity> config,
            Expression<Func<TEntity, TProp1>> field1,
            Expression<Func<TEntity, TProp2>> field2,
            Expression<Func<TEntity, TProp3>> field3,
            Expression<Func<TEntity, TProp4>> field4,
            Expression<Func<TEntity, TProp5>> field5,
            string name = null,
            bool isUnique = false)
            where TEntity : class
        {
            return config.HasIndex(
                fieldsExpressions: new LambdaExpression[] { field1, field2, field3, field4, field5 },
                name: name,
                isUnique: isUnique);
        }

        private static EntityTypeConfiguration<TEntity> HasIndex<TEntity>(
            this EntityTypeConfiguration<TEntity> config,
            LambdaExpression[] fieldsExpressions,
            string name = null,
            bool isUnique = false)
            where TEntity : class
        {
            var fieldsProperties = fieldsExpressions
                .Select(x => (MemberExpression)(x.Body.RemoveConvert()))
                .Select(x => (PropertyInfo)x.Member)
                .ToList();

            name = name ?? ("IX-" + string.Join("-", fieldsProperties.Select(x => x.Name)));

            var i = 0;
            foreach (var prop in fieldsProperties)
            {
                var type = config.GetType();
                var propertyMethod = type.GetMethods()
                    .Where(x => x.Name == "Property")
                    .Select(x => new
                    {
                        Method = x,
                        PropertyType = GetPropertyTypeOfPropertyMethod(x),
                    })
                    .Where(x => x.PropertyType == prop.PropertyType
                        // check if required overload is: Property<T>(Expression<Func<TStructuralType, T>> propertyExpression) where T : struct
                        || !x.PropertyType.IsNullable() && !prop.PropertyType.IsNullable() && prop.PropertyType.IsValueType
                        // check if required overload is: Property<T>(Expression<Func<TStructuralType, T?>> propertyExpression) where T : struct
                        || x.PropertyType.IsNullable() && prop.PropertyType.IsNullable() && prop.PropertyType.GetNullableUnderlyingType().IsValueType)
                    .Select(x => x.Method)
                    .Single();

                if (propertyMethod.IsGenericMethod)
                    propertyMethod = propertyMethod.MakeGenericMethod(prop.PropertyType.GetNullableUnderlyingType() ?? prop.PropertyType);

                var propertyConfig = propertyMethod.Invoke(config, new[] { fieldsExpressions[i] }) as PrimitivePropertyConfiguration;

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