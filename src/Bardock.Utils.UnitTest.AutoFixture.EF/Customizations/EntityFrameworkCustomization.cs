using Ploeh.AutoFixture;
using System.Data.Entity;

namespace Bardock.Utils.UnitTest.AutoFixture.EF.Customizations
{
    /// <summary>
    /// A composite customization that adds <see cref="EntityFrameworkEntityConfigurationCustomization"/>,
    /// <see cref="IgnoreEntityNavigationPropsCustomization"/> and <see cref="IgnoreEntityKeysCustomization"/>
    /// customizations
    /// </summary>
    /// <typeparam name="TDbContext">The type of the database context.</typeparam>
    public class EntityFrameworkCustomization<TDbContext> : CompositeCustomization
        where TDbContext : DbContext
    {
        public EntityFrameworkCustomization()
            : base(
                new EntityConfigurationCustomization<TDbContext>(),
                new IgnoreEntityNavigationPropsCustomization<TDbContext>(),
                new IgnoreEntityKeysCustomization<TDbContext>())
        {
        }
    }
}