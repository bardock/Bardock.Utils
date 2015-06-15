using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bardock.Utils.UnitTest.Samples.SUT.Entities
{
    public class Customer
    {
        //public enum Status
        //{
        //    Active,
        //    Inactive,
        //    PendingActivation
        //}

        public int ID { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        [MinLength(100)]
        public string LastName { get; set; }

        //MaxLength configured by EF.FluentEntityConfiguration
        public string NickName { get; set; }

        [Range(1,100)]
        public int Age { get; set; }

        //MaxLength configured by EF.FluentEntityConfiguration
        [EmailAddress]
        public string Email { get; set; }

        public virtual List<Address> Addresses { get; set; }

        public DateTime CreatedOn { get; set; }

        //[EnumDataType(typeof(Status))]
        public int StatusID { get; set; }
    }
}