using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Bardock.Utils.Collections;
using Bardock.Utils.Web.Mvc.HtmlTags.Extensions;
using HtmlTags;
using Xunit;

namespace Bardock.Utils.Web.Mvc.HtmlTags.Tests
{
    public class HtmlTagHelperOfTModelTest
    {
        private class Model1 
        {
            public int? PropInt { get; set; }
            public DateTime? PropDate { get; set; }
            public bool PropBool { get; set; }
            public IEnumerable<int> PropIntList { get; set; }
        }

        /// <summary>
        /// Provides type inference when declaring a new expression
        /// </summary>
        private Expression<Func<T, TResult>> Expr<T, TResult>(Expression<Func<T, TResult>> exp)
        {
            return exp;
        }

        public void AssertValid(HtmlTag tag, string tagName, string name, string type = null, object value = null)
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
        public void HtmlTagFor_Input()
        {
            var helper = new HtmlTagHelper<Model1>();

            var propExpression = Expr((Model1 m) => m.PropInt);
            var tagName = "input";
            var tag = helper.HtmlTagFor(propExpression, tagName);

            AssertValid(tag, tagName, "PropInt");
        }

        [Fact]
        public void HtmlTagFor_Any()
        {
            var helper = new HtmlTagHelper<Model1>();

            var propExpression = Expr((Model1 m) => m.PropInt);
            var tagName = "any";
            var tag = helper.HtmlTagFor(propExpression, tagName);

            AssertValid(tag, tagName, "PropInt");
        }

        [Fact]
        public void HtmlTagFor_Input_Value_Int()
        {
            var model = new Model1() { PropInt = 1 };
            var helper = new HtmlTagHelper<Model1>(model);

            var propExpression = Expr((Model1 m) => m.PropInt);
            var tagName = "input";
            var tag = helper.HtmlTagFor(propExpression, tagName);

            AssertValid(tag, tagName, "PropInt", value: model.PropInt);
        }

        [Fact]
        public void HtmlTagFor_Input_Value_Date()
        {
            var model = new Model1() { PropDate = DateTime.UtcNow };
            var helper = new HtmlTagHelper<Model1>(model);

            var propExpression = Expr((Model1 m) => m.PropDate);
            var tagName = "input";
            var tag = helper.HtmlTagFor(propExpression, tagName);

            AssertValid(tag, tagName, "PropDate", value: model.PropDate);
        }

        [Fact]
        public void HtmlTagFor_Input_Value_Null()
        {
            var model = new Model1() { PropInt = null };
            var helper = new HtmlTagHelper<Model1>(model);

            var propExpression = Expr((Model1 m) => m.PropInt);
            var tagName = "input";
            var tag = helper.HtmlTagFor(propExpression, tagName);

            AssertValid(tag, tagName, "PropInt", value: model.PropInt);
        }

        [Fact]
        public void HtmlTagFor_Expression_MethodCall()
        {
            var helper = new HtmlTagHelper<Model1>();
            Assert.Throws<InvalidOperationException>(
                () => helper.HtmlTagFor(m => m.PropInt.ToString(), "input"));
        }

        [Fact]
        public void HtmlTagFor_Expression_Constant()
        {
            var helper = new HtmlTagHelper<Model1>();
            Assert.Throws<InvalidOperationException>(
                () => helper.HtmlTagFor(m => 1, "input"));
        }

        [Fact]
        public void TextAreaFor_Value_Int()
        {
            var model = new Model1() { PropInt = 1 };
            var helper = new HtmlTagHelper<Model1>(model);

            var propExpression = Expr((Model1 m) => m.PropInt);
            var tag = helper.TextAreaFor(propExpression);

            AssertValid(tag, "textarea", "PropInt", value: model.PropInt);
        }

        [Fact]
        public void Input_Text_InputType_Value_Int()
        {
            var model = new Model1() { PropInt = 1 };
            var helper = new HtmlTagHelper<Model1>(model);

            var propExpression = Expr((Model1 m) => m.PropInt);
            var type = InputType.Text;
            var tag = helper.InputFor(propExpression, type);

            AssertValid(tag, "input", "PropInt", type: "text", value: model.PropInt);
        }

        [Fact]
        public void Input_Text_Value_Int()
        {
            var model = new Model1() { PropInt = 1 };
            var helper = new HtmlTagHelper<Model1>(model);

            var propExpression = Expr((Model1 m) => m.PropInt);
            var type = "text";
            var tag = helper.InputFor(propExpression, type);

            AssertValid(tag, "input", "PropInt", type: type, value: model.PropInt);
        }

        [Fact]
        public void Input_Any_Value_Int()
        {
            var model = new Model1() { PropInt = 1 };
            var helper = new HtmlTagHelper<Model1>(model);

            var propExpression = Expr((Model1 m) => m.PropInt);
            var type = "any";
            var tag = helper.InputFor(propExpression, type);

            AssertValid(tag, "input", "PropInt", type: type, value: model.PropInt);
        }

        [Fact]
        public void TextBoxFor_Value_Int()
        {
            var model = new Model1() { PropInt = 1 };
            var helper = new HtmlTagHelper<Model1>(model);

            var propExpression = Expr((Model1 m) => m.PropInt);
            var tag = helper.TextBoxFor(propExpression);

            AssertValid(tag, "input", "PropInt", type: "text", value: model.PropInt);
        }

        [Fact]
        public void PasswordFor_Value_Int()
        {
            var model = new Model1() { PropInt = 1 };
            var helper = new HtmlTagHelper<Model1>(model);

            var propExpression = Expr((Model1 m) => m.PropInt);
            var tag = helper.PasswordFor(propExpression);

            AssertValid(tag, "input", "PropInt", type: "password", value: model.PropInt);
        }

        [Fact]
        public void HiddenFor_Value_Int()
        {
            var model = new Model1() { PropInt = 1 };
            var helper = new HtmlTagHelper<Model1>(model);

            var propExpression = Expr((Model1 m) => m.PropInt);
            var tag = helper.HiddenFor(propExpression);

            AssertValid(tag, "input", "PropInt", type: "hidden", value: model.PropInt);
        }

        [Fact]
        public void RadioFor_Value_Int()
        {
            var model = new Model1() { PropInt = 1 };
            var helper = new HtmlTagHelper<Model1>(model);

            var propExpression = Expr((Model1 m) => m.PropInt);
            var tag = helper.RadioFor(propExpression);

            AssertValid(tag, "input", "PropInt", type: "radio", value: model.PropInt);
        }

        [Fact]
        public void RadioFor_Value_Bool()
        {
            var model = new Model1() { PropBool = true };
            var helper = new HtmlTagHelper<Model1>(model);

            var propExpression = Expr((Model1 m) => m.PropBool);
            var tag = helper.RadioFor(propExpression);

            AssertValid(tag, "input", "PropBool", type: "radio", value: model.PropBool);
            Assert.False(tag.Checked());
        }

        [Fact]
        public void RadioFor_Value_Bool_Checked()
        {
            var model = new Model1() { PropBool = true };
            var helper = new HtmlTagHelper<Model1>(model);

            var propExpression = Expr((Model1 m) => m.PropBool);
            var tag = helper.RadioFor(propExpression, isChecked: true);

            AssertValid(tag, "input", "PropBool", type: "radio", value: model.PropBool);
            Assert.True(tag.Checked());
        }

        [Fact]
        public void CheckBoxFor_Value_Int()
        {
            var model = new Model1() { PropInt = 1 };
            var helper = new HtmlTagHelper<Model1>(model);

            var propExpression = Expr((Model1 m) => m.PropInt);
            var tag = helper.CheckBoxFor(propExpression);

            AssertValid(tag, "input", "PropInt", type: "checkbox", value: model.PropInt);
        }

        [Fact]
        public void CheckBoxFor_Value_Bool()
        {
            var model = new Model1() { PropBool = true };
            var helper = new HtmlTagHelper<Model1>(model);

            var propExpression = Expr((Model1 m) => m.PropBool);
            var tag = helper.CheckBoxFor(propExpression);

            AssertValid(tag, "input", "PropBool", type: "checkbox", value: model.PropBool);
            Assert.False(tag.Checked());
        }

        [Fact]
        public void CheckBoxFor_Value_Bool_Checked()
        {
            var model = new Model1() { PropBool = true };
            var helper = new HtmlTagHelper<Model1>(model);

            var propExpression = Expr((Model1 m) => m.PropBool);
            var tag = helper.CheckBoxFor(propExpression, isChecked: true);

            AssertValid(tag, "input", "PropBool", type: "checkbox", value: model.PropBool);
            Assert.True(tag.Checked());
        }

        private enum Enum1
        {
            Option1 = 1,
            Option2 = 2,
            Option3 = 3
        }

        [Fact]
        public void SelectFor()
        {
            var model = new Model1() { PropInt = 2 };
            var helper = new HtmlTagHelper<Model1>(model);

            var propExpression = Expr((Model1 m) => m.PropInt);
            var tag = helper.SelectFor(
                propExpression, 
                OptionsList.CreateForEnum<Enum1>());

            AssertValid(tag, "select", "PropInt");

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
        public void SelectFor_Null()
        {
            var model = new Model1() { PropInt = null };
            var helper = new HtmlTagHelper<Model1>(model);

            var propExpression = Expr((Model1 m) => m.PropInt);
            var tag = helper.SelectFor(
                propExpression,
                OptionsList.CreateForEnum<Enum1>());

            AssertValid(tag, "select", "PropInt");

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
        public void SelectFor_Null_DefaultValue()
        {
            var model = new Model1() { PropInt = null };
            var helper = new HtmlTagHelper<Model1>(model);

            var propExpression = Expr((Model1 m) => m.PropInt);
            var tag = helper.SelectFor(
                propExpression,
                OptionsList.CreateForEnum<Enum1>(),
                defaultValue: 2);

            AssertValid(tag, "select", "PropInt");

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
        public void SelectFor_NotNull_DefaultValue()
        {
            var model = new Model1() { PropInt = 1 };
            var helper = new HtmlTagHelper<Model1>(model);

            var propExpression = Expr((Model1 m) => m.PropInt);
            var tag = helper.SelectFor(
                propExpression,
                OptionsList.CreateForEnum<Enum1>(),
                defaultValue: 2);

            AssertValid(tag, "select", "PropInt");

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
        public void CheckBoxListFor()
        {
            var model = new Model1() { PropIntList = Coll.Array(2, 3) };
            var helper = new HtmlTagHelper<Model1>(model);

            var propExpression = Expr((Model1 m) => m.PropIntList);
            var tag = helper.CheckBoxListFor(
                propExpression,
                OptionsList.CreateForEnum<Enum1>());

            Assert.Equal(3, tag.Children.Count());
            CheckBoxListTagTest.AssertValidOption(tag.Options.Skip(0).First(), name: "PropIntList", display: "Option1", value: 1, isChecked: false);
            CheckBoxListTagTest.AssertValidOption(tag.Options.Skip(1).First(), name: "PropIntList", display: "Option2", value: 2, isChecked: true);
            CheckBoxListTagTest.AssertValidOption(tag.Options.Skip(2).First(), name: "PropIntList", display: "Option3", value: 3, isChecked: true);
        }

        [Fact]
        public void CheckBoxListFor_Null()
        {
            var model = new Model1() { PropIntList = null };
            var helper = new HtmlTagHelper<Model1>(model);

            var propExpression = Expr((Model1 m) => m.PropIntList);
            var tag = helper.CheckBoxListFor(
                propExpression,
                OptionsList.CreateForEnum<Enum1>());

            Assert.Equal(3, tag.Children.Count());
            CheckBoxListTagTest.AssertValidOption(tag.Options.Skip(0).First(), name: "PropIntList", display: "Option1", value: 1, isChecked: false);
            CheckBoxListTagTest.AssertValidOption(tag.Options.Skip(1).First(), name: "PropIntList", display: "Option2", value: 2, isChecked: false);
            CheckBoxListTagTest.AssertValidOption(tag.Options.Skip(2).First(), name: "PropIntList", display: "Option3", value: 3, isChecked: false);
        }

        [Fact]
        public void CheckBoxListFor_Null_DefaultValue()
        {
            var model = new Model1() { PropIntList = null };
            var helper = new HtmlTagHelper<Model1>(model);

            var propExpression = Expr((Model1 m) => m.PropIntList);
            var tag = helper.CheckBoxListFor(
                propExpression,
                OptionsList.CreateForEnum<Enum1>(),
                defaultValues: Coll.Array(1, 2, 4));

            Assert.Equal(3, tag.Children.Count());
            CheckBoxListTagTest.AssertValidOption(tag.Options.Skip(0).First(), name: "PropIntList", display: "Option1", value: 1, isChecked: true);
            CheckBoxListTagTest.AssertValidOption(tag.Options.Skip(1).First(), name: "PropIntList", display: "Option2", value: 2, isChecked: true);
            CheckBoxListTagTest.AssertValidOption(tag.Options.Skip(2).First(), name: "PropIntList", display: "Option3", value: 3, isChecked: false);
        }

        [Fact]
        public void CheckBoxListFor_NotNull_DefaultValue()
        {
            var model = new Model1() { PropIntList = Coll.Array(2, 3) };
            var helper = new HtmlTagHelper<Model1>(model);

            var propExpression = Expr((Model1 m) => m.PropIntList);
            var tag = helper.CheckBoxListFor(
                propExpression,
                OptionsList.CreateForEnum<Enum1>(),
                defaultValues: Coll.Array(1, 2, 4));

            Assert.Equal(3, tag.Children.Count());
            CheckBoxListTagTest.AssertValidOption(tag.Options.Skip(0).First(), name: "PropIntList", display: "Option1", value: 1, isChecked: false);
            CheckBoxListTagTest.AssertValidOption(tag.Options.Skip(1).First(), name: "PropIntList", display: "Option2", value: 2, isChecked: true);
            CheckBoxListTagTest.AssertValidOption(tag.Options.Skip(2).First(), name: "PropIntList", display: "Option3", value: 3, isChecked: true);
        }
    }
}
