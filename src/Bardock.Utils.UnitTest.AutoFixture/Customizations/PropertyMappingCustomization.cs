using Bardock.Utils.Linq.Expressions;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bardock.Utils.UnitTest.AutoFixture.Customizations
{
    public class MemberMapping
    {
        public MemberInfo SourceMember { get; private set; }

        public MemberInfo DestinationMember { get; private set; }

        public MemberMapping(MemberInfo sourceMember, MemberInfo destinationMember)
        {
            SourceMember = sourceMember;
            DestinationMember = destinationMember;
        }
    }

    public class PropertyMappingCustomization : ICustomization
    {
        private Type _sourceType;
        private Type _destinationType;
        private IEnumerable<MemberMapping> _mappings;

        public PropertyMappingCustomization(Type sourceType, Type destinationType, IEnumerable<MemberMapping> mappings)
        {
            _sourceType = sourceType;
            _destinationType = destinationType;
            _mappings = mappings;
        }

        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(new PropertyMappingSpecimenBuilder(_sourceType, _destinationType, _mappings));
        }
    }

    public class PropertyMappingSpecimenBuilder : ISpecimenBuilder
    {
        private Type _sourceType;
        private Type _destinationType;
        private IEnumerable<MemberMapping> _mappings;

        public PropertyMappingSpecimenBuilder(
            Type sourceType, 
            Type destinationType, 
            IEnumerable<MemberMapping> mappings)
        {
            _sourceType = sourceType;
            _destinationType = destinationType;
            _mappings = mappings;
        }

        public object Create(object request, ISpecimenContext context)
        {
            if (_mappings == null || !_mappings.Any())
            {
                return new NoSpecimen(request);
            }

            var pi = request as PropertyInfo;
            if (pi == null || pi.DeclaringType != _sourceType)
            {
                return new NoSpecimen(request);
            }

            var prop = _mappings
                        .Where(p => p.SourceMember is PropertyInfo)
                        .Where(p => p.SourceMember == pi)
                        .FirstOrDefault();

            if (prop == null)
            {
                return new NoSpecimen(request);
            }

            return context.Resolve(prop.DestinationMember);
        }
    }



}
