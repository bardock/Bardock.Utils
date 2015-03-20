using System;
using System.Collections.Generic;

namespace Bardock.Utils.UnitTest.Samples.SUT.Entities
{
    public class Customer
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string Email { get; set; }

        public List<Address> Addresses { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}