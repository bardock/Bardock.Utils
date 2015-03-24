using Bardock.Utils.UnitTest.Data.EF.Effort.DataLoaders;
using Bardock.Utils.UnitTest.Samples.SUT.Entities;
using System.Collections.Generic;

namespace Bardock.Utils.UnitTest.Samples.Fixtures.DataLoaders
{
    public class AddressDataLoader : IEntityDataLoader<Address>
    {
        public IEnumerable<Address> GetData()
        {
            return new List<Address>();
        }
    }
}