using Bardock.Utils.Extensions;
using Ploeh.AutoFixture.Kernel;
using System.Reflection;

namespace Bardock.Utils.UnitTest.Samples.Fixtures.DataLoaders
{
    internal class LoaderFixture : Ploeh.AutoFixture.Fixture
    {
        public LoaderFixture()
        {
            this.Customizations.Add(new IgnoreComplexProperties());
        }

        private class IgnoreComplexProperties : ISpecimenBuilder
        {
            public object Create(object request,
                ISpecimenContext context)
            {
                var pi = request as ParameterInfo;
                if (pi != null && !pi.ParameterType.IsPrimitive())
                {
                    return new OmitSpecimen();
                }
                return new NoSpecimen(request);
            }
        }
    }
}