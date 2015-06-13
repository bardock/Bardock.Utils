using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bardock.Utils.UnitTest.Samples.Fixtures.Customizations
{
    public class EmailCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(new EmailSpecimenBuilder());
        }
    }

    public class EmailSpecimenBuilder : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            var pi = request as PropertyInfo;
            if (pi != null 
                && pi.PropertyType == typeof(string)
                && (pi.Name == "Email"
                    || !pi.CustomAttributes.Any(a => a.AttributeType.IsAssignableFrom(typeof(EmailAddressAttribute)))))
            {
                return string.Format("{0}@fobar.com", context.Resolve(typeof(string)));
            }

            return new NoSpecimen(request);
        }
    }

}
