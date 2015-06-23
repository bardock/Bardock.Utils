using Ploeh.AutoFixture.Kernel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Bardock.Utils.UnitTest.AutoFixture.SpecimenBuilders
{
    /// <summary>
    /// A specimen builder that generates specimens for class string members that have a ValidationAttribute
    /// like <see cref="StringLengthAttribute"/>, <see cref="MinLengthAttribute"/>, <see cref="MaxLengthAttribute"/> and
    /// <see cref="EmailAddressAttribute"/>
    /// </summary>
    public class StringDataAnnotationsSpecimenBuilder : ISpecimenBuilder
    {
        private string _emailHost;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringDataAnnotationsSpecimenBuilder"/> class.
        /// </summary>
        /// <param name="emailHost">The email host.</param>
        public StringDataAnnotationsSpecimenBuilder(string emailHost = "@email.com")
        {
            this._emailHost = emailHost;
        }

        /// <summary>
        /// Creates a new specimen based on a request.
        /// </summary>
        /// <param name="request">The request that describes what to create.</param>
        /// <param name="context">A context that can be used to create other specimens.</param>
        /// <returns>
        /// A valid specimen for the requested member if possible; otherwise a <see cref="T:Ploeh.AutoFixture.Kernel.NoSpecimen" /> instance.
        /// </returns>
        public virtual object Create(object request, ISpecimenContext context)
        {
            var pi = request as PropertyInfo;
            if (pi != null
                && pi.PropertyType == typeof(string)
                && IsValidType(pi))
            {
                return CreateString(pi, context);
            }

            return new NoSpecimen(request);
        }

        protected virtual bool IsValidType(PropertyInfo pi)
        {
            return pi.CustomAttributes.Any(a => typeof(ValidationAttribute).IsAssignableFrom(a.AttributeType));
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
            var minLength = GetStringMinLength(pi, context);
            var maxLength = GetStringMaxLength(pi, context);
            var isEmail = IsEmail(pi, context);

            if (!minLength.HasValue && !maxLength.HasValue && !isEmail)
                return new NoSpecimen(pi);

            string specimen = null;
            if (isEmail)
            {
                minLength = GetStringMinLength(pi, context) ?? 0;

                specimen = string.Format(
                    "{0}{1}",
                    context.Resolve(
                        new ConstrainedStringRequest(
                            (minLength ?? 0),
                            (maxLength ?? int.MaxValue) - _emailHost.Length)),
                    _emailHost);
            }
            else
            {
                specimen = (string)context.Resolve(
                                new ConstrainedStringRequest(
                                    minLength ?? 0,
                                    maxLength ?? int.MaxValue));
            }
            return specimen;
        }
    }
}