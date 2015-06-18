using System;
using System.Data.Entity;
using System.Linq;

namespace Bardock.Utils.UnitTest.AutoFixture.EF.Helpers
{
    internal static class DbContextHelper
    {
        /// <summary>
        /// Determines whether a type corresponds to a mapped entity in entity framework.
        /// </summary>
        /// <typeparam name="TDbContext">The type of the database context.</typeparam>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static bool IsMappedEntity<TDbContext>(this Type type)
            where TDbContext : DbContext
        {
            return typeof(TDbContext)
                .GetProperties()
                .Select(prop => prop.PropertyType)
                .Where(propType => propType.IsGenericType)
                .Where(propType => typeof(IDbSet<>).IsAssignableFrom(propType.GetGenericTypeDefinition()))
                .Select(propType => propType.GetGenericArguments()[0])
                .Any(entityType => entityType.IsAssignableFrom(type));
        }
    }
}