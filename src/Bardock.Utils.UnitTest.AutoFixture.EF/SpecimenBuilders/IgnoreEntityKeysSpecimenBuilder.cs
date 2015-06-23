using Bardock.Utils.UnitTest.AutoFixture.EF.Helpers;
using Ploeh.AutoFixture.Kernel;
using System.Data.Entity;
using System.Reflection;

namespace Bardock.Utils.UnitTest.AutoFixture.EF.SpecimenBuilders
{
    /// <summary>
    /// A specimen builder that specifies null value for Key properties in entity types of <see cref="DbContext"/>
    /// </summary>
    /// <typeparam name="TDbContext">The type of the database context.</typeparam>
    public class IgnoreEntityKeysSpecimenBuilder<TDbContext> : ISpecimenBuilder
        where TDbContext : DbContext
    {
        /// <summary>
        /// Creates a new specimen based on a request.
        /// </summary>
        /// <param name="request">The request that describes what to create.</param>
        /// <param name="context">A context that can be used to create other specimens.</param>
        /// <returns>
        /// The requested specimen if possible; otherwise a <see cref="T:Ploeh.AutoFixture.Kernel.NoSpecimen" /> instance.
        /// </returns>
        public object Create(object request, ISpecimenContext context)
        {
            var pi = request as PropertyInfo;
            if (pi == null)
            {
                return new NoSpecimen(request);
            }

            var propName = pi.GetGetMethod().Name;
            if ((propName.EndsWith("ID") || propName.EndsWith("Id")) && pi.DeclaringType.IsMappedEntity<TDbContext>())
            {
                return null;
            }
            return new NoSpecimen(request);
        }
    }
}