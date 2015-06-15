using Bardock.Utils.UnitTest.AutoFixture.Customizations;
using Bardock.Utils.UnitTest.AutoFixture.EF.Helpers;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;

namespace Bardock.Utils.UnitTest.AutoFixture.EF.Customizations
{
    public class EntityFrameworkEntityConfigurationCustomization<TDbContext> : ICustomization
        where TDbContext : DbContext
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(new EntityFrameworkEntityConfigurationSpecimenBuilder<TDbContext>());
        }
    }

    public class EntityFrameworkEntityConfigurationSpecimenBuilder<TDbContext> : DataAnnotationsSpecimenBuilder
        where TDbContext : DbContext
    {
        protected override bool IsValidType(PropertyInfo pi)
        {
            return pi.DeclaringType.IsMappedEntity<TDbContext>();
        }

        protected override int? GetStringMaxLength(PropertyInfo pi, ISpecimenContext context)
        {
            var length = base.GetStringMaxLength(pi, context);

            //DataAnnotations win vs EF
            if (length.HasValue)
            {
                return length;
            }

            var edmProperty = GetProperty(pi, context);
            if (edmProperty != null)
            {
                length = edmProperty.MaxLength.Value;
            }

            return length;
        }

        private EdmProperty GetProperty(PropertyInfo pi, ISpecimenContext context)
        {
            return ((IObjectContextAdapter)context.Create<TDbContext>())
                        .ObjectContext
                        .MetadataWorkspace
                        .GetItemCollection(DataSpace.CSpace)
                        .Where(gi => gi.BuiltInTypeKind == BuiltInTypeKind.EntityType)
                        .Cast<EntityType>()
                        .SelectMany(et =>
                            et.Properties
                                .Where(p => p.PrimitiveType.ClrEquivalentType == typeof(string))
                                .Where(p => p.Name == pi.Name)
                                .Where(p => p.MaxLength.HasValue))
                        .FirstOrDefault();
        }
    }
}