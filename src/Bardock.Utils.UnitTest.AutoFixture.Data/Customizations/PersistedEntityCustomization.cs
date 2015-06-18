using Bardock.Utils.UnitTest.AutoFixture.Data.SpecimenBuilders;
using Ploeh.AutoFixture;
using System;
using System.Reflection;

namespace Bardock.Utils.UnitTest.Data.AutoFixture.Customizations
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
            var db = fixture.Create<IDataContextScopeFactory>();
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
}