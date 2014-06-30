using System.Linq;
using Bardock.Utils.Types;
using Xunit;

namespace Bardock.Utils.Tests.Types
{
    public class EnumTypeTest
    {
        public enum EnumInt
        {
            Option1 = 1,
            Option2 = 2,
            OptionMax = int.MaxValue
        }

        public enum EnumLong : long
        {
            Option1 = 1,
            Option2 = 2,
            OptionMax = long.MaxValue
        }

        [Fact]
        public void Int()
        {
            var et = EnumType.Create<EnumInt>();

            Assert.Equal(3, et.Count());

            var firstOption = et.First();
            var secondOption = et.Skip(1).First();
            var thirdOption = et.Skip(2).First();

            Assert.Equal(EnumInt.Option1, firstOption.Enum);
            Assert.Equal("Option1", firstOption.Name);
            Assert.Equal(1, firstOption.Value);

            Assert.Equal(EnumInt.Option2, secondOption.Enum);
            Assert.Equal("Option2", secondOption.Name);
            Assert.Equal(2, secondOption.Value);

            Assert.Equal(EnumInt.OptionMax, thirdOption.Enum);
            Assert.Equal("OptionMax", thirdOption.Name);
            Assert.Equal(int.MaxValue, thirdOption.Value);
        }

        [Fact]
        public void Long()
        {
            var et = EnumType.Create<EnumLong, long>();

            Assert.Equal(3, et.Count());

            var firstOption = et.First();
            var secondOption = et.Skip(1).First();
            var thirdOption = et.Skip(2).First();

            Assert.Equal(EnumLong.Option1, firstOption.Enum);
            Assert.Equal("Option1", firstOption.Name);
            Assert.Equal(1, firstOption.Value);

            Assert.Equal(EnumLong.Option2, secondOption.Enum);
            Assert.Equal("Option2", secondOption.Name);
            Assert.Equal(2, secondOption.Value);

            Assert.Equal(EnumLong.OptionMax, thirdOption.Enum);
            Assert.Equal("OptionMax", thirdOption.Name);
            Assert.Equal(long.MaxValue, thirdOption.Value);
        }
    }
}
