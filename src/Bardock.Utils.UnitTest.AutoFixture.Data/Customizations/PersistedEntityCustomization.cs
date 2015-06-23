using Bardock.Utils.UnitTest.AutoFixture.Data.SpecimenBuilders;
using Ploeh.AutoFixture;
using System;
using System.Reflection;

namespace Bardock.Utils.UnitTest.Data.AutoFixture.Customizations
{
    /// <summary>
    /// A customization that persists a specified entity type
    /// </summary>
    public class PersistedEntityCustomization : ICustomization
    {
        private Type _entityType;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistedEntityCustomization"/> class.
        /// </summary>
        /// <param name="entityType">Type of the entity to be persisted.</param>
        public PersistedEntityCustomization(Type entityType)
        {
            this._entityType = entityType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistedEntityCustomization"/> class.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public PersistedEntityCustomization(ParameterInfo parameter)
            : this(parameter.ParameterType) { }

        /// <summary>
        /// Customizes the specified fixture by adding a <see cref="PersistedEntitySpecimenBuilder"/> instance.
        /// </summary>
        /// <param name="fixture">The fixture to customize.</param>
        public void Customize(IFixture fixture)
        {
            var db = fixture.Create<IDataContextScopeFactory>();
            fixture.Customizations.Add(new PersistedEntitySpecimenBuilder(this._entityType, db));
        }
    }

    /// <summary>
    /// A customization that persists a specified entity type
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public class PersistedEntityCustomization<TEntity> : PersistedEntityCustomization
    {
        public PersistedEntityCustomization()
            : base(typeof(TEntity))
        { }
    }
}