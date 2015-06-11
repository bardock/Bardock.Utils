using System.Data.Entity;
using System.Reflection;
using Bardock.Utils.UnitTest.Samples.SUT.Infra;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace Bardock.Utils.UnitTest.Samples.Fixtures.Customizations
{
    public class IgnoreEntityKeysCustomization<TDbContext> : ICustomization
        where TDbContext : DbContext
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(new IgnoreEntityKeysSpecimenBuilder<TDbContext>());
        }
    }

    public class IgnoreEntityKeysSpecimenBuilder<TDbContext> : ISpecimenBuilder
        where TDbContext : DbContext
    {
        public object Create(object request, ISpecimenContext context)
        {
            var pi = request as PropertyInfo;
            if (pi == null)
            {
                return new NoSpecimen(request);
            }

            var propName = pi.GetGetMethod().Name;
            if ((propName.EndsWith("ID") || propName.EndsWith("Id")) && pi.DeclaringType.IsMappedEntity<DataContext>())
            {
                return null;
            }
            return new NoSpecimen(request);
        }
    }
}