using AutoMapper;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bardock.Utils.UnitTest.AutoFixture.AutoMapper.Customizations
{
    public class AutoMapperCustomization : ICustomization
    {
        private Type _sourceType;
        private Type _destinationType;

        public AutoMapperCustomization(Type sourceType, Type destinationType)
        {
            _sourceType = sourceType;
            _destinationType = destinationType;
        }

        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(new AutoMapperSpecimenBuilder(_sourceType, _destinationType));
        }
    }

    public class AutoMapperSpecimenBuilder : ISpecimenBuilder
    {
        private Type _sourceType;
        private Type _destinationType;

        public AutoMapperSpecimenBuilder(Type sourceType, Type destinationType)
        {
            _sourceType = sourceType;
            _destinationType = destinationType;
        }

        public object Create(object request, ISpecimenContext context)
        {
            var pi = request as PropertyInfo;

            if (pi == null || pi.DeclaringType != _sourceType)
            {
                return new NoSpecimen(request);
            }

            var map = Mapper.GetAllTypeMaps()
                        .Where(m => m.SourceType == _sourceType)
                        .Where(m => m.DestinationType == _destinationType)
                        .FirstOrDefault();

            if (map == null)
            {
                return new NoSpecimen(request);
            }

            var prop = map.GetPropertyMaps()
                        .Where(p => p.SourceMember is PropertyInfo)
                        .Where(p => p.SourceMember == pi)
                        .FirstOrDefault();

            if (prop == null)
            {
                return new NoSpecimen(request);
            }

            return context.Resolve(prop.DestinationProperty.MemberInfo);
        }
    }

}
