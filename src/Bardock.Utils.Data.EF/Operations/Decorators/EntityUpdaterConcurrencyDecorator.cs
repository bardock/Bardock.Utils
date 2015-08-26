using System;

namespace Bardock.Utils.Data.EF.Operations.Decorators
{
    public class EntityUpdaterConcurrencyDecorator : IEntityUpdater
    {
        private IEntityUpdater _entityUpdater;

        public EntityUpdaterConcurrencyDecorator(
            IEntityUpdater entityUpdater)
        {
            if (entityUpdater == null)
            {
                throw new ArgumentNullException("entityUpdater");
            }

            _entityUpdater = entityUpdater;
        }

        //TODO: RowVersion

        public void Update(DbContextBase dbCtx, object e)
        {
            _entityUpdater.Update(dbCtx, e);
        }
    }
}