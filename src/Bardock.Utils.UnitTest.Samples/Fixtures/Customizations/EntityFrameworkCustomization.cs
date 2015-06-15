using Ploeh.AutoFixture;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bardock.Utils.UnitTest.Samples.Fixtures.Customizations
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
