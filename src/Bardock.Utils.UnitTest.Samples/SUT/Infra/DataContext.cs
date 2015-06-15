using Bardock.Utils.UnitTest.Samples.SUT.Entities;
using System.Data.Common;
using System.Data.Entity;

namespace Bardock.Utils.UnitTest.Samples.SUT.Infra
{
    public class DataContext : DbContext
    {
        public DataContext()
            : base()
        {
        }

        public DataContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public DataContext(DbConnection connection)
            : base(connection, true)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Customer>()
                .Property(p => p.NickName)
                .HasMaxLength(100);

            modelBuilder
                .Entity<Customer>()
                .Property(p => p.Email)
                .HasMaxLength(15);
        }

        public IDbSet<Country> Countries { get; set; }

        public IDbSet<Customer> Customers { get; set; }
    }
}