using System;
using System.Collections.Generic;

namespace Bardock.Utils.UnitTest.Samples.SUT.Entities
{
    public class Order
    {
        public int ID { get; set; }

        public Customer Customer { get; set; }

        public List<Product> Products { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}