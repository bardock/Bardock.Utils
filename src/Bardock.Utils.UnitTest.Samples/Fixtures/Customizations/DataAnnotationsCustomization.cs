using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bardock.Utils.UnitTest.Samples.Fixtures.Customizations
{
    public class DataAnnotationsCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(new DataAnnotationsSpecimenBuilder());
        }
    }

    public class DataAnnotationsSpecimenBuilder : ISpecimenBuilder
    {
        private string _emailHost;

        public DataAnnotationsSpecimenBuilder(string emailHost = "@email.com")
        {
            this._emailHost = emailHost;
        }

        public virtual object Create(object request, ISpecimenContext context)
        {
            var pi = request as PropertyInfo;
            if (pi != null
                && IsValidType(pi))
            {
                if (pi.PropertyType == typeof(string))
                {
                    return CreateString(pi, context);
                }
                else
                {
                    return CreateObject(pi, context);
                }
            }

            return new NoSpecimen(request);
        }

        protected virtual bool IsValidType(PropertyInfo pi)
        {
            return pi.CustomAttributes.Any(a => typeof(ValidationAttribute).IsAssignableFrom(a.AttributeType));
        }

        protected virtual object GetMinimumRange(PropertyInfo pi, ISpecimenContext context)
        {
            var attr = pi.GetCustomAttribute<RangeAttribute>(inherit: true);
            if (attr != null)
            {
                return attr.Minimum;
            }

            return null;
        }

        protected virtual object GetMaximumRange(PropertyInfo pi, ISpecimenContext context)
        {
            var attr = pi.GetCustomAttribute<RangeAttribute>(inherit: true);
            if (attr != null)
            {
                return attr.Maximum;
            }

            return null;
        }

        protected virtual object CreateObject(PropertyInfo pi, ISpecimenContext context)
        {
            var minimum = GetMinimumRange(pi, context);
            var maximum = GetMaximumRange(pi, context);

            if (minimum != null && maximum != null)
            {
                return new RangedNumberRequest(pi.PropertyType, minimum, maximum);
            }
            else
            {
                return new NoSpecimen();
            }
        }

        protected virtual int? GetStringMinLength(PropertyInfo pi, ISpecimenContext context)
        {
            var stringLengthAttr = pi.GetCustomAttribute<StringLengthAttribute>(inherit: true);
            if (stringLengthAttr != null)
            {
                return stringLengthAttr.MinimumLength;
            }

            var minLengthAttr = pi.GetCustomAttribute<MinLengthAttribute>(inherit: true);
            if (minLengthAttr != null)
            {
                return minLengthAttr.Length;
            }

            return null;
        }

        protected virtual int? GetStringMaxLength(PropertyInfo pi, ISpecimenContext context)
        {
            var stringLengthAttr = pi.GetCustomAttribute<StringLengthAttribute>(inherit: true);
            if (stringLengthAttr != null)
            {
                return stringLengthAttr.MaximumLength;
            }

            var maxLengthAttr = pi.GetCustomAttribute<MaxLengthAttribute>(inherit: true); 
            if (maxLengthAttr != null)
            {
                return maxLengthAttr.Length;
            }

            return null;
        }

        protected virtual bool IsEmail(PropertyInfo pi, ISpecimenContext context)
        {
            return pi.Name.EndsWith("Email")
                || pi.CustomAttributes.Any(a => typeof(EmailAddressAttribute).IsAssignableFrom(a.AttributeType));
        }

        protected virtual object CreateString(PropertyInfo pi, ISpecimenContext context)
        {
            var minLength = GetStringMinLength(pi, context) ?? 0;
            var maxLength = GetStringMaxLength(pi, context) ?? int.MaxValue;
            var isEmail = IsEmail(pi, context);

            string specimen = null;
            if (isEmail)
            {
                specimen = string.Format(
                    "{0}{1}",
                    context.Resolve(
                        new ConstrainedStringRequest(
                            minLength,
                            maxLength - _emailHost.Length)),
                    _emailHost);
            }
            else
            {
                specimen = (string)context.Resolve(
                                new ConstrainedStringRequest(
                                    minLength,
                                    maxLength));
            }
            return specimen;
        }
    }
}
