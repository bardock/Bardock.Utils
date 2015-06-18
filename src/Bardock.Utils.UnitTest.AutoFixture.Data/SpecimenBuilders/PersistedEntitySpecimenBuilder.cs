using Bardock.Utils.UnitTest.Data;
using Ploeh.AutoFixture.Kernel;
using System;
using System.Reflection;

namespace Bardock.Utils.UnitTest.AutoFixture.Data.SpecimenBuilders
{
    public class PersistedEntitySpecimenBuilder : ISpecimenBuilder
    {
        private Type _entityType;
        private IDataContextScopeFactory _dataScope;

        public PersistedEntitySpecimenBuilder(Type entityType, IDataContextScopeFactory dataScope)
        {
            _entityType = entityType;
            _dataScope = dataScope;
        }

        public object Create(object request, ISpecimenContext context)
        {
            var pi = request as ParameterInfo;
            if (pi == null || pi.ParameterType != this._entityType)
            {
                return new NoSpecimen(request);
            }

            var e = context.Resolve(this._entityType);
            try
            {
                using (var s = _dataScope.CreateDefault())
                {
                    s.Db.Add(e);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return e;
        }
    }
}