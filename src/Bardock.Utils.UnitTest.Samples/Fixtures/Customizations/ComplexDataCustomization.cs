using Ploeh.AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bardock.Utils.UnitTest.Samples.Fixtures.Customizations
{
    public class ComplexDataCustomization : CompositeCustomization
    {
        public ComplexDataCustomization()
            : base(new EmailCustomization()) { }
    }
}
