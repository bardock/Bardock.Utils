using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bardock.Utils.Web.Mvc.HtmlTags.Extensions;
using HtmlTags;
using Xunit;

namespace Bardock.Utils.Web.Mvc.HtmlTags.Tests.Extensions
{
    public class HtmlTagExtensionsTest
    {
        [Fact]
        public void BoolAttr_Get_Unspecified()
        {
            var tag = new HtmlTag("input");
            var value = tag.BoolAttr("disabled");
            Assert.False(value);
        }

        [Fact]
        public void BoolAttr_Get_Empty()
        {
            var tag = new HtmlTag("input").Attr("disabled", "");
            var value = tag.BoolAttr("disabled");
            Assert.True(value);
        }

        [Fact]
        public void BoolAttr_Get_Space()
        {
            var tag = new HtmlTag("input").Attr("disabled", " ");
            var value = tag.BoolAttr("disabled");
            Assert.True(value);
        }

        [Fact]
        public void BoolAttr_Get_Zero()
        {
            var tag = new HtmlTag("input").Attr("disabled", "0");
            var value = tag.BoolAttr("disabled");
            Assert.True(value);
        }

        [Fact]
        public void BoolAttr_Get_False()
        {
            var tag = new HtmlTag("input").Attr("disabled", "false");
            var value = tag.BoolAttr("disabled");
            Assert.True(value);
        }

        [Fact]
        public void BoolAttr_Get_A()
        {
            var tag = new HtmlTag("input").Attr("disabled", "a");
            var value = tag.BoolAttr("disabled");
            Assert.True(value);
        }

        [Fact]
        public void BoolAttr_Get_True()
        {
            var tag = new HtmlTag("input").Attr("disabled", "true");
            var value = tag.BoolAttr("disabled");
            Assert.True(value);
        }

        [Fact]
        public void BoolAttr_Set_True()
        {
            var tag = new HtmlTag("input").BoolAttr("disabled", true);
            var value = tag.BoolAttr("disabled");
            Assert.True(value);
        }

        [Fact]
        public void BoolAttr_Set_False()
        {
            var tag = new HtmlTag("input").BoolAttr("disabled", false);
            var value = tag.BoolAttr("disabled");
            Assert.False(value);
        }

        [Fact]
        public void BoolAttr_Set_True_Then_False()
        {
            var tag = new HtmlTag("input").BoolAttr("disabled", true).BoolAttr("disabled", false);
            var value = tag.BoolAttr("disabled");
            Assert.False(value);
            Assert.False(tag.HasAttr("disabled"));
        }
    }
}
