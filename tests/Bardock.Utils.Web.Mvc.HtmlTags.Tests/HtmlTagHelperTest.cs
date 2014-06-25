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
        public void AssertValid(HtmlTag tag, string tagName, string name, string type = "")
        {
            Assert.Equal(tagName, tag.TagName());
            Assert.Equal(name, tag.Attr("name"));
            Assert.Equal(name, tag.Attr("id"));
            Assert.Equal(type, tag.Attr("type"));
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
        }

        [Fact]
        public void CheckBox()
        {
            var helper = new HtmlTagHelper();
            var name = "name1";
            var tag = helper.CheckBox(name);

            AssertValid(tag, "input", name, "checkbox");
        }
    }
}
