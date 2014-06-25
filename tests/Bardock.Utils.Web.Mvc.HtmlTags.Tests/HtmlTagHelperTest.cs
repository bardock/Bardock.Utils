using Bardock.Utils.Web.Mvc.HtmlTags.Extensions;
using System;
using System.Linq;
using Xunit;
using HtmlTags;
using System.Web.Mvc;

namespace Bardock.Utils.Web.Mvc.HtmlTags.Tests
{
    public class HtmlTagHelperTest
    {
        private class Model1
        {
            public int? PropInt { get; set; }
            public DateTime? PropDate { get; set; }
            public bool PropBool { get; set; }
        }

        public void AssertValid(HtmlTag tag, string tagName, string name, string type = "", object value = null)
        {
            Assert.Equal(tagName, tag.TagName());
            Assert.Equal(name, tag.Attr("name"));
            Assert.Equal(name, tag.Attr("id"));

            if (type == null)
                Assert.False(tag.HasAttr("type"));
            else
                Assert.Equal(type, tag.Attr("type"));

            if (value == null)
                Assert.True(!tag.HasAttr("value") || tag.Attr("value") == "");
            else
                Assert.Equal(value.ToString(), tag.Attr("value"));
        }

        [Fact]
        public void HtmlTag_Input()
        {
            var helper = new HtmlTagHelper();
            var tagName = "input";
            var name = "name1";
            var tag = helper.HtmlTag(tagName, name);

            AssertValid(tag, tagName, name);
        }

        [Fact]
        public void HtmlTag_Input_Value_Int()
        {
            var model = new Model1() { PropInt = 1 };
            var helper = new HtmlTagHelper(model);
            var tagName = "input";
            var name = "PropInt";
            var tag = helper.HtmlTag(tagName, name);

            AssertValid(tag, tagName, name, value: model.PropInt);
        }

        [Fact]
        public void HtmlTag_Input_Value_Date()
        {
            var model = new Model1() { PropDate = DateTime.UtcNow };
            var helper = new HtmlTagHelper(model);
            var tagName = "input";
            var name = "PropDate";
            var tag = helper.HtmlTag(tagName, name);

            AssertValid(tag, tagName, name, value: model.PropDate);
        }

        [Fact]
        public void HtmlTag_Input_Value_Null()
        {
            var model = new Model1() { PropInt = null };
            var helper = new HtmlTagHelper(model);
            var tagName = "input";
            var name = "PropInt";
            var tag = helper.HtmlTag(tagName, name);

            AssertValid(tag, tagName, name, value: model.PropInt);
        }

        [Fact]
        public void HtmlTag_Any()
        {
            var helper = new HtmlTagHelper();
            var tagName = "any";
            var name = "name2";
            var tag = helper.HtmlTag(tagName, name);

            AssertValid(tag, tagName, name);
        }

        [Fact]
        public void TextArea()
        {
            var helper = new HtmlTagHelper();
            var name = "name1";
            var tag = helper.TextArea(name);

            AssertValid(tag, "textarea", name);
        }

        [Fact]
        public void Input_Text_InputType()
        {
            var helper = new HtmlTagHelper();
            var name = "name1";
            var type = InputType.Text;
            var tag = helper.Input(type, name);

            AssertValid(tag, "input", name, "text");
        }

        [Fact]
        public void Input_Text()
        {
            var helper = new HtmlTagHelper();
            var name = "name1";
            var type = "text";
            var tag = helper.Input(type, name);

            AssertValid(tag, "input", name, type);
        }

        [Fact]
        public void Input_Any()
        {
            var helper = new HtmlTagHelper();
            var name = "name1";
            var type = "any";
            var tag = helper.Input(type, name);

            AssertValid(tag, "input", name, type);
        }

        [Fact]
        public void TextBox()
        {
            var helper = new HtmlTagHelper();
            var name = "name1";
            var tag = helper.TextBox(name);

            AssertValid(tag, "input", name, "text");
        }

        [Fact]
        public void Password()
        {
            var helper = new HtmlTagHelper();
            var name = "name1";
            var tag = helper.Password(name);

            AssertValid(tag, "input", name, "password");
        }

        [Fact]
        public void Hidden()
        {
            var helper = new HtmlTagHelper();
            var name = "name1";
            var tag = helper.Hidden(name);

            AssertValid(tag, "input", name, "hidden");
        }

        [Fact]
        public void Radio()
        {
            var helper = new HtmlTagHelper();
            var name = "name1";
            var tag = helper.Radio(name);

            AssertValid(tag, "input", name, "radio");
            Assert.False(tag.HasAttr("checked"));
        }

        [Fact]
        public void Radio_Checked()
        {
            var helper = new HtmlTagHelper();
            var name = "name1";
            var tag = helper.Radio(name, isChecked: true);

            AssertValid(tag, "input", name, "radio");
            Assert.Equal("true", tag.Attr("checked"));
        }

        [Fact]
        public void CheckBox()
        {
            var helper = new HtmlTagHelper();
            var name = "name1";
            var tag = helper.CheckBox(name);

            AssertValid(tag, "input", name, "checkbox");
            Assert.False(tag.HasAttr("checked"));
        }

        [Fact]
        public void CheckBox_Checked()
        {
            var helper = new HtmlTagHelper();
            var name = "name1";
            var tag = helper.CheckBox(name, isChecked: true);

            AssertValid(tag, "input", name, "checkbox");
            Assert.Equal("true", tag.Attr("checked"));
        }
    }
}
