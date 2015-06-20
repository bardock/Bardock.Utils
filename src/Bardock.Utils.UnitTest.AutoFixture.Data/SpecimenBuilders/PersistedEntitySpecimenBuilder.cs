using Bardock.Utils.UnitTest.Data;
using Ploeh.AutoFixture.Kernel;
using System;
using System.Reflection;

namespace Bardock.Utils.UnitTest.AutoFixture.Data.SpecimenBuilders
{
    /// <summary>
    /// An <see cref="ISpecimenBuilder"/> that persists a specified entity type using a <see cref="IDataContextScopeFactory"/>
    /// </summary>
    public class PersistedEntitySpecimenBuilder : ISpecimenBuilder
    {
        private Type _entityType;
        private IDataContextScopeFactory _dataContextScopeFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistedEntitySpecimenBuilder"/> class.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="dataContextScopeFactory">The data context scope factory.</param>
        public PersistedEntitySpecimenBuilder(Type entityType, IDataContextScopeFactory dataContextScopeFactory)
        {
            _entityType = entityType;
            _dataContextScopeFactory = dataContextScopeFactory;
        }

        /// <summary>
        /// Creates a new specimen based on a request.
        /// If request is a <see cref="ParameterInfo"/> of specified entity type, resolve entity instance and persist it.
        /// </summary>
        /// <param name="request">The request that describes what to create.</param>
        /// <param name="context">A context that can be used to create other specimens.</param>
        /// <returns>
        /// The requested specimen if possible; otherwise a <see cref="T:Ploeh.AutoFixture.Kernel.NoSpecimen" /> instance.
        /// </returns>
        public object Create(object request, ISpecimenContext context)
        {
            var pi = request as ParameterInfo;
            if (pi == null || pi.ParameterType != this._entityType)
            {
                return new NoSpecimen(request);
            }

            var e = context.Resolve(this._entityType);
            using (var s = _dataContextScopeFactory.CreateDefault())
            {
                s.Db.Add(e);
            }
            return e;
        }
    }
}