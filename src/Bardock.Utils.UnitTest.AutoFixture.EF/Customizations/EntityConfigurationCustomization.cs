using Bardock.Utils.UnitTest.AutoFixture.EF.SpecimenBuilders;
using Ploeh.AutoFixture;
using System.Data.Entity;

namespace Bardock.Utils.UnitTest.AutoFixture.EF.Customizations
{
    /// <summary>
    /// A customization that provides support for generating valid specimens for an entity framework <see cref="DbContext"/>
    /// </summary>
    /// <typeparam name="TDbContext">The type of the database context.</typeparam>
    public class EntityConfigurationCustomization<TDbContext> : ICustomization
        where TDbContext : DbContext
    {
        /// <summary>
        /// Customizes the specified fixture by adding support for generating valid specimens for an entity framework <see cref="DbContext"/>
        /// </summary>
        /// <param name="fixture">The fixture to customize.</param>
        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(new EntityConfigurationSpecimenBuilder<TDbContext>());
        }
    }
}