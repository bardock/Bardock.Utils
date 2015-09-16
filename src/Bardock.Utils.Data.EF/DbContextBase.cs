using Bardock.Utils.Data.EF.Exceptions.Mappers;
using Bardock.Utils.Data.EF.Operations;
using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Bardock.Utils.Data.EF.Annotations;
using Bardock.Utils.Extensions;

namespace Bardock.Utils.Data.EF
{
    public class DbContextBase : DbContext
    {
        private IExceptionMapper _exceptionMapper;
        private IEntityAdder _entityAdder;
        private IEntityUpdater _entityUpdater;
        private IEntityDeleter _entityDeleter;
        private IEntityDetacher _entityDetacher;

        protected DbContextBase(
            IExceptionMapper exceptionMapper = null,
            IEntityAdder entityAdder = null,
            IEntityUpdater entityUpdater = null,
            IEntityDeleter entityDeleter = null,
            IEntityDetacher entityDetacher = null)
            : base()
        {
            Init(exceptionMapper, entityAdder, entityUpdater, entityDeleter, entityDetacher);
        }

        public DbContextBase(
            string nameOrConnectionString,
            IExceptionMapper exceptionMapper = null,
            IEntityAdder entityAdder = null,
            IEntityUpdater entityUpdater = null,
            IEntityDeleter entityDeleter = null,
            IEntityDetacher entityDetacher = null)
            : base(nameOrConnectionString)
        {
            Init(exceptionMapper, entityAdder, entityUpdater, entityDeleter, entityDetacher);
        }

        public DbContextBase(
            DbConnection existingConnection,
            bool contextOwnsConnection,
            IExceptionMapper exceptionMapper = null,
            IEntityAdder entityAdder = null,
            IEntityUpdater entityUpdater = null,
            IEntityDeleter entityDeleter = null,
            IEntityDetacher entityDetacher = null)
            : base(existingConnection, contextOwnsConnection)
        {
            Init(exceptionMapper, entityAdder, entityUpdater, entityDeleter, entityDetacher);
        }

        public DbContextBase(
            ObjectContext objectContext,
            bool dbContextOwnsObjectContext,
            IExceptionMapper exceptionMapper = null,
            IEntityAdder entityAdder = null,
            IEntityUpdater entityUpdater = null,
            IEntityDeleter entityDeleter = null,
            IEntityDetacher entityDetacher = null)
            : base(objectContext, dbContextOwnsObjectContext)
        {
            Init(exceptionMapper, entityAdder, entityUpdater, entityDeleter, entityDetacher);
        }

        public DbContextBase(
            string nameOrConnectionString,
            DbCompiledModel model,
            IExceptionMapper exceptionMapper = null,
            IEntityAdder entityAdder = null,
            IEntityUpdater entityUpdater = null,
            IEntityDeleter entityDeleter = null,
            IEntityDetacher entityDetacher = null)
            : base(nameOrConnectionString, model)
        {
            Init(exceptionMapper, entityAdder, entityUpdater, entityDeleter, entityDetacher);
        }

        public DbContextBase(
            DbConnection existingConnection,
            DbCompiledModel model,
            bool contextOwnsConnection,
            IExceptionMapper exceptionMapper = null,
            IEntityAdder entityAdder = null,
            IEntityUpdater entityUpdater = null,
            IEntityDeleter entityDeleter = null,
            IEntityDetacher entityDetacher = null)
            : base(existingConnection, model, contextOwnsConnection)
        {
            Init(exceptionMapper, entityAdder, entityUpdater, entityDeleter, entityDetacher);
        }

        protected virtual void SetExceptionMapper(IExceptionMapper exceptionMapper)
        {
            _exceptionMapper = exceptionMapper ?? new NullExceptionMapper();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            SetupDbSetTables(modelBuilder);
        }

        /// <summary>
        /// Setups table and schema name for each db set
        /// </summary>
        protected void SetupDbSetTables(DbModelBuilder modelBuilder)
        {
            foreach (var p in this.GetType().GetProperties()
                .Where(prop => prop.PropertyType.IsAssignableToGenericType(typeof(IDbSet<>)))
                .Select(prop => new { Prop = prop, TableAttr = prop.GetCustomAttribute<DbSetTableAttribute>() })
                .Where(x => x.TableAttr != null))
            {
                var entityType = p.Prop.PropertyType.GetGenericArguments()[0];
                var tableName = p.TableAttr.Name ?? entityType.Name;

                modelBuilder
                    .Entity(entityType)
                    .ToTable(tableName, p.TableAttr.Schema);
            }
        }

        protected virtual void SetEntityAdder(IEntityAdder entityAdder)
        {
            _entityAdder = entityAdder ?? new EntityAdder();
        }

        protected virtual void SetEntityUpdater(IEntityUpdater entityUpdater)
        {
            _entityUpdater = entityUpdater ?? new EntityUpdater();
        }

        protected virtual void SetEntityDeleter(IEntityDeleter entityDeleter)
        {
            _entityDeleter = entityDeleter ?? new EntityDeleter();
        }

        protected virtual void SetEntityDetacher(IEntityDetacher entityDetacher)
        {
            _entityDetacher = entityDetacher ?? new EntityDetacher();
        }

        private void Init(
            IExceptionMapper exceptionMapper,
            IEntityAdder entityAdder,
            IEntityUpdater entityUpdater,
            IEntityDeleter entityDeleter,
            IEntityDetacher entityDetacher)
        {
            SetExceptionMapper(exceptionMapper);
            SetEntityAdder(entityAdder);
            SetEntityUpdater(entityUpdater);
            SetEntityDeleter(entityDeleter);
            SetEntityDetacher(entityDetacher);
        }

        internal void Delete(object e)
        {
            _entityDeleter.Delete(this, e);
        }

        internal void Update(object e)
        {
            _entityUpdater.Update(this, e);
        }

        internal void Detach(object e)
        {
            _entityDetacher.Detach(this, e);
        }

        internal void Add(object e)
        {
            _entityAdder.Add(this, e);
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