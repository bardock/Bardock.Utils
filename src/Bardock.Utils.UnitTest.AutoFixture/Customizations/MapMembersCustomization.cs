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

    public class MapMembersCustomization : ICustomization
    {
        private Type _sourceType;
        private Type _destinationType;
        private IEnumerable<MemberMapping> _mappings;

        public MapMembersCustomization(Type sourceType, Type destinationType, IEnumerable<MemberMapping> mappings)
        {
            _sourceType = sourceType;
            _destinationType = destinationType;
            _mappings = mappings;
        }

        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(new MapMembersSpecimenBuilder(_sourceType, _destinationType, _mappings));
        }
    }

    public class MapMembersCustomization<TSource, TDestination> : MapMembersCustomization
    {
        public MapMembersCustomization(IEnumerable<MemberMapping> mappings)
            : base(typeof(TSource), typeof(TDestination), mappings)
        { }
    }

    public class MapMembersSpecimenBuilder : ISpecimenBuilder
    {
        private Type _sourceType;
        private Type _destinationType;
        private IEnumerable<MemberMapping> _mappings;

        public MapMembersSpecimenBuilder(
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

            var mi = request as MemberInfo;
            if (mi == null || mi.DeclaringType != _sourceType)
            {
                return new NoSpecimen(request);
            }

            var mapping = _mappings
                        .Where(p => p.SourceMember == mi)
                        .FirstOrDefault();

            if (mapping == null)
            {
                return new NoSpecimen(request);
            }

            return context.Resolve(mapping.DestinationMember);
        }
    }



}
