using Bardock.Utils.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Bardock.Utils.Data.EF.Operations.Decorators
{
    public class EntityUpdaterConcurrencyDecorator : IEntityUpdater
    {
        private IEntityUpdater _entityUpdater;
        private string _propertyName;
        private IEnumerable<Type> _typesToExclude;

        public EntityUpdaterConcurrencyDecorator(
            IEntityUpdater entityUpdater,
            string propertyName = "RowVersion",
            IEnumerable<Type> typesToExclude = null)
        {
            Init(entityUpdater,propertyName,typesToExclude);
        }

        public EntityUpdaterConcurrencyDecorator(
            IEntityUpdater entityUpdater,
            Expression<Func<object, byte[]>> propertyExpr,
            IEnumerable<Type> typesToExclude = null)
        {
            if (propertyExpr == null)
                throw new ArgumentNullException("propertyExpr");

            Init(entityUpdater, ExpressionHelper.GetMemberName(propertyExpr), typesToExclude);
        }

        private void Init(
            IEntityUpdater entityUpdater,
            string propertyName,
            IEnumerable<Type> typesToExclude)
        {
            if (entityUpdater == null)
            {
                throw new ArgumentNullException("entityUpdater");
            }

            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentNullException("propertyName");
            }

            _entityUpdater = entityUpdater;
            _propertyName = propertyName;
            _typesToExclude = typesToExclude ?? Enumerable.Empty<Type>();
        }

        public void Update(DbContextBase dbCtx, object e)
        {
            var type = e.GetType();
            if (_typesToExclude.Any(t => t.IsAssignableFrom(type)))
            {
                var pi = type.GetProperty(_propertyName);
                if (pi != null)
                {
                    dbCtx.Entry(e).OriginalValues[_propertyName] = pi.GetValue(e);
                }
            }

            _entityUpdater.Update(dbCtx, e);
        }
    }
}