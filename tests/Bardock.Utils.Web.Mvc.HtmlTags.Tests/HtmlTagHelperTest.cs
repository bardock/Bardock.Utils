using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bardock.Utils.Collections;
using Bardock.Utils.Web.Mvc.HtmlTags.Extensions;
using HtmlTags;
using Xunit;

namespace Bardock.Utils.Web.Mvc.HtmlTags.Tests
{
    public class HtmlTagHelperTest
    {
        private class Model1
        {
            public int? PropInt { get; set; }
            public DateTime? PropDate { get; set; }
            public bool PropBool { get; set; }
            public IEnumerable<int> PropIntList { get; set; }
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
            Assert.False(tag.Checked());
        }

        [Fact]
        public void Radio_Checked()
        {
            var helper = new HtmlTagHelper();
            var name = "name1";
            var tag = helper.Radio(name, isChecked: true);

            AssertValid(tag, "input", name, "radio");
            Assert.True(tag.Checked());
        }

        [Fact]
        public void CheckBox()
        {
            var helper = new HtmlTagHelper();
            var name = "name1";
            var tag = helper.CheckBox(name);

            AssertValid(tag, "input", name, "checkbox");
            Assert.False(tag.Checked());
        }

        [Fact]
        public void CheckBox_Checked()
        {
            var helper = new HtmlTagHelper();
            var name = "name1";
            var tag = helper.CheckBox(name, isChecked: true);

            AssertValid(tag, "input", name, "checkbox");
            Assert.True(tag.Checked());
        }

        private enum Enum1
        {
            Option1 = 1,
            Option2 = 2,
            Option3 = 3
        }

        [Fact]
        public void Select()
        {
            var model = new Model1() { PropInt = 2 };
            var helper = new HtmlTagHelper(model);

            var name = "PropInt";
            var tag = helper.Select(
                name,
                OptionsList.CreateForEnum<Enum1>());

            AssertValid(tag, "select", name);

            var firstChild = tag.Children.First();
            var secondChild = tag.Children.Skip(1).First();
            var thirdChild = tag.Children.Skip(2).First();

            Assert.Equal("Option1", firstChild.Text());
            Assert.True(firstChild.ValueIsEqual(1));
            Assert.False(firstChild.HasAttr("selected"));
            Assert.Equal("Option2", secondChild.Text());
            Assert.True(secondChild.ValueIsEqual(2));
            Assert.True(secondChild.HasAttr("selected"));
            Assert.Equal("Option3", thirdChild.Text());
            Assert.True(thirdChild.ValueIsEqual(3));
            Assert.False(thirdChild.HasAttr("selected"));
        }

        [Fact]
        public void Select_Null()
        {
            var model = new Model1() { PropInt = null };
            var helper = new HtmlTagHelper(model);

            var name = "PropInt";
            var tag = helper.Select(
                name,
                OptionsList.CreateForEnum<Enum1>());

            AssertValid(tag, "select", name);

            var firstChild = tag.Children.First();
            var secondChild = tag.Children.Skip(1).First();
            var thirdChild = tag.Children.Skip(2).First();

            Assert.Equal("Option1", firstChild.Text());
            Assert.True(firstChild.ValueIsEqual(1));
            Assert.False(firstChild.HasAttr("selected"));
            Assert.Equal("Option2", secondChild.Text());
            Assert.True(secondChild.ValueIsEqual(2));
            Assert.False(secondChild.HasAttr("selected"));
            Assert.Equal("Option3", thirdChild.Text());
            Assert.True(thirdChild.ValueIsEqual(3));
            Assert.False(thirdChild.HasAttr("selected"));
        }

        [Fact]
        public void Select_Null_DefaultValue()
        {
            var model = new Model1() { PropInt = null };
            var helper = new HtmlTagHelper(model);

            var name = "PropInt";
            var tag = helper.Select(
                name,
                OptionsList.CreateForEnum<Enum1>(),
                defaultValue: 2);

            AssertValid(tag, "select", name);

            var firstChild = tag.Children.First();
            var secondChild = tag.Children.Skip(1).First();
            var thirdChild = tag.Children.Skip(2).First();

            Assert.Equal("Option1", firstChild.Text());
            Assert.True(firstChild.ValueIsEqual(1));
            Assert.False(firstChild.HasAttr("selected"));
            Assert.Equal("Option2", secondChild.Text());
            Assert.True(secondChild.ValueIsEqual(2));
            Assert.True(secondChild.HasAttr("selected"));
            Assert.Equal("Option3", thirdChild.Text());
            Assert.True(thirdChild.ValueIsEqual(3));
            Assert.False(thirdChild.HasAttr("selected"));
        }

        [Fact]
        public void Select_NotNull_DefaultValue()
        {
            var model = new Model1() { PropInt = 1 };
            var helper = new HtmlTagHelper(model);

            var name = "PropInt";
            var tag = helper.Select(
                name,
                OptionsList.CreateForEnum<Enum1>(),
                defaultValue: 2);

            AssertValid(tag, "select", name);

            var firstChild = tag.Children.First();
            var secondChild = tag.Children.Skip(1).First();
            var thirdChild = tag.Children.Skip(2).First();

            Assert.Equal("Option1", firstChild.Text());
            Assert.True(firstChild.ValueIsEqual(1));
            Assert.True(firstChild.HasAttr("selected"));
            Assert.Equal("Option2", secondChild.Text());
            Assert.True(secondChild.ValueIsEqual(2));
            Assert.False(secondChild.HasAttr("selected"));
            Assert.Equal("Option3", thirdChild.Text());
            Assert.True(thirdChild.ValueIsEqual(3));
            Assert.False(thirdChild.HasAttr("selected"));
        }

        [Fact]
        public void CheckBoxList()
        {
            var model = new Model1() { PropIntList = Coll.Array(2, 3) };
            var helper = new HtmlTagHelper(model);

            var name = "PropIntList";
            var tag = helper.CheckBoxList(
                name,
                OptionsList.CreateForEnum<Enum1>());

            Assert.Equal(3, tag.Children.Count());
            CheckBoxListTagTest.AssertValidOption(tag.Options.Skip(0).First(), name: name, display: "Option1", value: 1, isChecked: false);
            CheckBoxListTagTest.AssertValidOption(tag.Options.Skip(1).First(), name: name, display: "Option2", value: 2, isChecked: true);
            CheckBoxListTagTest.AssertValidOption(tag.Options.Skip(2).First(), name: name, display: "Option3", value: 3, isChecked: true);
        }

        [Fact]
        public void CheckBoxList_Null()
        {
            var model = new Model1() { PropIntList = null };
            var helper = new HtmlTagHelper(model);

            var name = "PropIntList";
            var tag = helper.CheckBoxList(
                name,
                OptionsList.CreateForEnum<Enum1>());

            Assert.Equal(3, tag.Children.Count());
            CheckBoxListTagTest.AssertValidOption(tag.Options.Skip(0).First(), name: name, display: "Option1", value: 1, isChecked: false);
            CheckBoxListTagTest.AssertValidOption(tag.Options.Skip(1).First(), name: name, display: "Option2", value: 2, isChecked: false);
            CheckBoxListTagTest.AssertValidOption(tag.Options.Skip(2).First(), name: name, display: "Option3", value: 3, isChecked: false);
        }

        [Fact]
        public void CheckBoxList_Null_DefaultValue()
        {
            var model = new Model1() { PropIntList = null };
            var helper = new HtmlTagHelper(model);

            var name = "PropIntList";
            var tag = helper.CheckBoxList(
                name,
                OptionsList.CreateForEnum<Enum1>(),
                defaultValues: Coll.Array(1, 2, 4));

            Assert.Equal(3, tag.Children.Count());
            CheckBoxListTagTest.AssertValidOption(tag.Options.Skip(0).First(), name: name, display: "Option1", value: 1, isChecked: true);
            CheckBoxListTagTest.AssertValidOption(tag.Options.Skip(1).First(), name: name, display: "Option2", value: 2, isChecked: true);
            CheckBoxListTagTest.AssertValidOption(tag.Options.Skip(2).First(), name: name, display: "Option3", value: 3, isChecked: false);
        }

        [Fact]
        public void CheckBoxList_NotNull_DefaultValue()
        {
            var model = new Model1() { PropIntList = Coll.Array(2, 3) };
            var helper = new HtmlTagHelper(model);

            var name = "PropIntList";
            var tag = helper.CheckBoxList(
                name,
                OptionsList.CreateForEnum<Enum1>(),
                defaultValues: Coll.Array(1, 2, 4));

            Assert.Equal(3, tag.Children.Count());
            CheckBoxListTagTest.AssertValidOption(tag.Options.Skip(0).First(), name: name, display: "Option1", value: 1, isChecked: false);
            CheckBoxListTagTest.AssertValidOption(tag.Options.Skip(1).First(), name: name, display: "Option2", value: 2, isChecked: true);
            CheckBoxListTagTest.AssertValidOption(tag.Options.Skip(2).First(), name: name, display: "Option3", value: 3, isChecked: true);
        }
    }
}
