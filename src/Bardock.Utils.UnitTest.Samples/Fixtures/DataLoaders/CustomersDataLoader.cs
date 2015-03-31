using System;
using System.Collections.Generic;
using Bardock.Utils.UnitTest.Data.EF.Effort.DataLoaders;
using Bardock.Utils.UnitTest.Samples.SUT.Entities;

namespace Bardock.Utils.UnitTest.Samples.Fixtures.DataLoaders
{
    public class CustomersDataLoader : IEntityDataLoader<Customer>
    {
        internal static Customer AdultFromUS = new Customer()
        {
            ID = 1,
            Age = 30,
            FirstName = "Customer",
            LastName = "1",
            Email = "asd@asd.com",
            CreatedOn = new DateTime(2000, 1, 1)
        };

        public IEnumerable<Customer> GetData()
        {
            yield return AdultFromUS;
        }
    }
}