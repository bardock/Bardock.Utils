namespace Bardock.Utils.UnitTest.Samples.SUT.Entities
{
    public class Address
    {
        public int ID { get; set; }

        public int CustomerID { get; set; }

        public virtual Customer Customer { get; set; }

        public string Line1 { get; set; }

        public string Line2 { get; set; }

        public string State { get; set; }

        public int CountryID { get; set; }

        public virtual Country Country { get; set; }
    }
}