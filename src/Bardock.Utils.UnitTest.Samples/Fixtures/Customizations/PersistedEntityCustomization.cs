using Bardock.Utils.UnitTest.Data;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using System;
using System.Reflection;

namespace Bardock.Utils.UnitTest.Samples.Fixtures.Customizations
{
    public class PersistedEntityCustomization : ICustomization
    {
        private Type _entityType;

        public PersistedEntityCustomization(Type entityType)
        {
            this._entityType = entityType;
        }

        public PersistedEntityCustomization(ParameterInfo parameter)
            : this(parameter.ParameterType) { }

        public void Customize(IFixture fixture)
        {
            var db = fixture.Create<IDataContextWrapper>();
            fixture.Customizations.Add(new PersistedEntitySpecimenBuilder(this._entityType, db));
        }
    }

    public class PersistedEntityCustomization<TEntity> : PersistedEntityCustomization
    {
        public PersistedEntityCustomization()
            : base(typeof(TEntity))
        {
        }
    }

    public class PersistedEntitySpecimenBuilder : ISpecimenBuilder
    {
        private Type _entityType;
        private IDataContextWrapper _db;

        public PersistedEntitySpecimenBuilder(Type entityType, IDataContextWrapper db)
        {
            _entityType = entityType;
            _db = db;
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
                _db.Add(e).Save();
            }
            catch(Exception ex)
            {
                throw;
            }

            return e;
        }
    }
}