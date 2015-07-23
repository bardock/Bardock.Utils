using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using Bardock.Utils.Data.EF.Exceptions.Mappers;

namespace Bardock.Utils.Data.EF
{
    public class DbContextBase : DbContext
    {
        private IExceptionMapper _exceptionMapper;

        protected DbContextBase()
            : base()
        {
            SetExceptionMapper(null);
        }

        public DbContextBase(
            IExceptionMapper exceptionMapper)
            : base()
        {
            SetExceptionMapper(exceptionMapper);
        }

        public DbContextBase(
            string nameOrConnectionString,
            IExceptionMapper exceptionMapper = null)
            : base(nameOrConnectionString)
        {
            SetExceptionMapper(exceptionMapper);
        }

        public DbContextBase(
            DbConnection existingConnection,
            bool contextOwnsConnection,
            IExceptionMapper exceptionMapper = null)
            : base(existingConnection, contextOwnsConnection)
        {
            SetExceptionMapper(exceptionMapper);
        }

        public DbContextBase(
            ObjectContext objectContext,
            bool dbContextOwnsObjectContext,
            IExceptionMapper exceptionMapper = null)
            : base(objectContext, dbContextOwnsObjectContext)
        {
            SetExceptionMapper(exceptionMapper);
        }

        public DbContextBase(
            string nameOrConnectionString,
            DbCompiledModel model,
            IExceptionMapper exceptionMapper = null)
            : base(nameOrConnectionString, model)
        {
            SetExceptionMapper(exceptionMapper);
        }

        public DbContextBase(
            DbConnection existingConnection,
            DbCompiledModel model,
            bool contextOwnsConnection,
            IExceptionMapper exceptionMapper = null)
            : base(existingConnection, model, contextOwnsConnection)
        {
            SetExceptionMapper(exceptionMapper);
        }

        private void SetExceptionMapper(IExceptionMapper exceptionMapper)
        {
            _exceptionMapper = exceptionMapper ?? new NullExceptionMapper();
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (Exception ex)
            {
                throw _exceptionMapper.Map(ex);
            }
        }

        public override async Task<int> SaveChangesAsync()
        {
            try
            {
                return await base.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw _exceptionMapper.Map(ex);
            }
        }
    }
}