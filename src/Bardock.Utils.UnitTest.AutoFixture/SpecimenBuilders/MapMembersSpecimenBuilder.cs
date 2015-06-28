using Bardock.Utils.UnitTest.AutoFixture.Customizations;
using Ploeh.AutoFixture.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bardock.Utils.UnitTest.AutoFixture.Extensions;

namespace Bardock.Utils.UnitTest.AutoFixture.SpecimenBuilders
{
    /// <summary>
    /// A specimen builder that generates mapped specimens of the <paramref name="sourceType"/> to the <paramref name="destinationType"/>
    /// given the specified <paramref name="mappings"/>
    /// </summary>
    public class MapMembersSpecimenBuilder : ISpecimenBuilder
    {
        private Type _sourceType;
        private Type _destinationType;
        private IEnumerable<MemberMapping> _mappings;
        private object _destination;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapMembersSpecimenBuilder"/> class.
        /// </summary>
        /// <param name="sourceType">Type of the source.</param>
        /// <param name="destinationType">Type of the destination.</param>
        /// <param name="mappings">The mappings.</param>
        public MapMembersSpecimenBuilder(Type sourceType, Type destinationType, IEnumerable<MemberMapping> mappings)
        {
            _sourceType = sourceType;
            _destinationType = destinationType;
            _mappings = mappings;
            _destination = null;
        }

        /// <summary>
        /// Creates a new specimen based on a request.
        /// </summary>
        /// <param name="request">The request that describes what to create.</param>
        /// <param name="context">A context that can be used to create other specimens.</param>
        /// <returns>
        /// A valid specimen for the requested member if possible; otherwise a <see cref="T:Ploeh.AutoFixture.Kernel.NoSpecimen" /> instance.
        /// </returns>
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

            //Since AutoFixture registers an Omitter when a Member is
            //customized via fixture.Customize<T>(c => c.With(x => x.<Member>, <value>))
            //then if a call to context.Resolve is made using that member an
            //OmitSpecimen is returned
            //return context.Resolve(mapping.DestinationMember); -> OmitSpecimen
            if (_destination == null)
                //fetch an instance of destinationType, for performance
                //we will always use the same instance to provide the
                //values for the requested members
                _destination = context.Resolve(_destinationType);

            return mapping.DestinationMember.GetValue(_destination);            
        }
    }
}