using Ploeh.AutoFixture;
using System.Data.Entity;

namespace Bardock.Utils.UnitTest.AutoFixture.EF.Customizations
{
    public class EntityFrameworkCustomization<TDbContext> : CompositeCustomization
        where TDbContext : DbContext
    {
        public EntityFrameworkCustomization()
            : base(
                new EntityFrameworkEntityConfigurationCustomization<TDbContext>(),
                new IgnoreEntityNavigationPropsCustomization<TDbContext>(),
                new IgnoreEntityKeysCustomization<TDbContext>())
        {
        }
    }
}