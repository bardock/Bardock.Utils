using System;
using System.Data.Entity;
using System.Linq;

namespace Bardock.Utils.UnitTest.Samples.Fixtures.Customizations
{
    public static class DbContextHelper
    {
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