using Bardock.Utils.UnitTest.AutoFixture.EF.SpecimenBuilders;
using Ploeh.AutoFixture;
using System.Data.Entity;

namespace Bardock.Utils.UnitTest.AutoFixture.EF.Customizations
{
    /// <summary>
    /// A customization that provides support for generating specimens
    /// without value for Key properties in entity types of <see cref="DbContext"/>
    /// </summary>
    /// <typeparam name="TDbContext">The type of the database context.</typeparam>
    public class IgnoreEntityKeysCustomization<TDbContext> : ICustomization
        where TDbContext : DbContext
    {
        /// <summary>
        /// Customizes the specified fixture by adding support for generating specimens
        /// without value for Key properties in entity types of <see cref="DbContext"/>.
        /// </summary>
        /// <param name="fixture">The fixture to customize.</param>
        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(new IgnoreEntityKeysSpecimenBuilder<TDbContext>());
        }
    }
}