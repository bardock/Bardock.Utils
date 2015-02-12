using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Bardock.Utils.Extensions;
using Bardock.Utils.Globalization;

namespace Bardock.Utils.Tests.Extensions
{
    public class StringExtensionsTest
    {    
        [Fact]
        public void Cut_0()
        {
            var r = "asdfg".Cut(0);
            Assert.Empty(r);
        }

        [Fact]
        public void Cut_Middle()
        {
            var r = "asdfg".Cut(3);
            Assert.Equal("asd", r);
        }

        [Fact]
        public void Cut_End()
        {
            var r = "asdfg".Cut(5);
            Assert.Equal("asdfg", r);
        }

        [Fact]
        public void Cut_Overflow()
        {
            var r = "asdfg".Cut(6);
            Assert.Equal("asdfg", r);
        }

        [Fact]
        public void Cut_Empty()
        {
            var r = "".Cut(5);
            Assert.Empty(r);
        }

        [Fact]
        public void CutEnd_0()
        {
            var r = "asdfg".CutEnd(0);
            Assert.Empty(r);
        }

        [Fact]
        public void CutEnd_Middle()
        {
            var r = "asdfg".CutEnd(3);
            Assert.Equal("dfg", r);
        }

        [Fact]
        public void CutEnd_End()
        {
            var r = "asdfg".CutEnd(5);
            Assert.Equal("asdfg", r);
        }

        [Fact]
        public void CutEnd_Overflow()
        {
            var r = "asdfg".CutEnd(6);
            Assert.Equal("asdfg", r);
        }

        [Fact]
        public void CutEnd_Empty()
        {
            var r = "".CutEnd(5);
            Assert.Empty(r);
        }

        [Fact]
        public void Contains_Empty()
        {
            var r = "a".Contains("", StringComparison.CurrentCulture);
            Assert.True(r);
        }

        [Fact]
        public void Contains_EmptyEmpty()
        {
            var r = "".Contains("", StringComparison.CurrentCulture);
            Assert.True(r);
        }

        [Fact]
        public void Contains_Empty_Fail()
        {
            var r = "".Contains("a", StringComparison.CurrentCulture);
            Assert.False(r);
        }

        [Fact]
        public void Contains_Equal()
        {
            var r = "a".Contains("a", StringComparison.CurrentCulture);
            Assert.True(r);
        }

        [Fact]
        public void Contains_Middle()
        {
            var r = "asd".Contains("s", StringComparison.CurrentCulture);
            Assert.True(r);
        }

        [Fact]
        public void Contains_Beginning()
        {
            var r = "asd".Contains("a", StringComparison.CurrentCulture);
            Assert.True(r);
        }

        [Fact]
        public void Contains_End()
        {
            var r = "asd".Contains("d", StringComparison.CurrentCulture);
            Assert.True(r);
        }

        [Fact]
        public void Contains_Middle_CaseSensitive()
        {
            var r = "asd".Contains("S", StringComparison.CurrentCulture);
            Assert.False(r);
        }

        [Fact]
        public void Contains_Middle_IgnoreCase()
        {
            var r = "asd".Contains("S", StringComparison.CurrentCultureIgnoreCase);
            Assert.True(r);
        }

        [Fact]
        public void IsLike_SourceWithDiacritcs_IgnoreDiacritics() 
        {
            var r = "tést".IsLike("e", IgnoreDiacritics: true);
            Assert.True(r);
        }

        [Fact]
        public void IsLike_QueryWithDiacritcs_IgnoreDiacritics()
        {
            var r = "test".IsLike("té", IgnoreDiacritics: true);
            Assert.True(r);
        }

        [Fact]
        public void IsLike_Source_IgnoreCase()
        {
            var r = "tést".IsLike("E", IgnoreCase: true);
            Assert.True(r);
        }

        [Fact]
        public void IsLike_Query_IgnoreCase()
        {
            var r = "TÉST".IsLike("te",  IgnoreCase: true);
            Assert.True(r);
        }

        [Fact]
        public void IsLike_SourceWithDiacritcs_IgnoreDiacritics_Fail()
        {
            var r = "tést".IsLike("a", IgnoreDiacritics: true);
            Assert.False(r);
        }

        [Fact]
        public void IsLike_QueryWithDiacritcs_IgnoreDiacritics_Fail()
        {
            var r = "test".IsLike("tá", IgnoreDiacritics: true);
            Assert.False(r);
        }

        [Fact]
        public void IsLike_Source_IgnoreCase_Fail()
        {
            var r = "tést".IsLike("A", IgnoreCase: true);
            Assert.False(r);
        }

        [Fact]
        public void IsLike_Query_IgnoreCase_Fail()
        {
            var r = "TÉST".IsLike("ta",  IgnoreCase: true);
            Assert.False(r);
        }

        [Fact]
        public void IsLike_SourceWithDiacritcs_NoIgnoreDiacritics_Fail()
        {
            var r = "tést".IsLike("e", IgnoreDiacritics: false);
            Assert.False(r);
        }

        [Fact]
        public void IsLike_Source_NoIgnoreCase_Fail()
        {
            var r = "TEST".IsLike("e", IgnoreCase: false);
            Assert.False(r);
        }
    }
}
