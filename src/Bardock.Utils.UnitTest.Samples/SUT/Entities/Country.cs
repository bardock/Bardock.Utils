namespace Bardock.Utils.UnitTest.Samples.SUT.Entities
{
    public class Country
    {
        public enum Options
        {
            Canada = 1,
            USA = 2
        }

        public int ID { get; set; }

        public string Name { get; set; }

        public string IsoCode { get; set; }
    }
}