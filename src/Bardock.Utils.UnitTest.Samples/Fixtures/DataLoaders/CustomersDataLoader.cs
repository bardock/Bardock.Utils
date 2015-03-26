using Bardock.Utils.UnitTest.Data.EF.Effort.DataLoaders;
using Bardock.Utils.UnitTest.Samples.SUT.Entities;
using System.Collections.Generic;

namespace Bardock.Utils.UnitTest.Samples.Fixtures.DataLoaders
{
    public class CustomersDataLoader : IEntityDataLoader<Customer>
    {
        public IEnumerable<Customer> GetData()
        {
            return new List<Customer>();
        }
    }
}