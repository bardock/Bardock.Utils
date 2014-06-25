using Bardock.Utils.Web.Mvc.HtmlTags.Extensions;
using System;
using System.Linq;
using Xunit;
using HtmlTags;
using System.Web.Mvc;
using System.Linq.Expressions;

namespace Bardock.Utils.Web.Mvc.HtmlTags.Tests
{
    public class HtmlTagTModelHelperTest
    {
        private class Model1 
        {
            public int? PropInt { get; set; }
            public DateTime? PropDate { get; set; }
            public bool PropBool { get; set; }
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
            var helper = new HtmlTagTModelHelper<Model1>();

            var propExpression = Expr((Model1 m) => m.PropInt);
            var tagName = "input";
            var tag = helper.HtmlTagFor(propExpression, tagName);

            AssertValid(tag, tagName, "PropInt");
        }

        [Fact]
        public void HtmlTagFor_Any()
        {
            var helper = new HtmlTagTModelHelper<Model1>();

            var propExpression = Expr((Model1 m) => m.PropInt);
            var tagName = "any";
            var tag = helper.HtmlTagFor(propExpression, tagName);

            AssertValid(tag, tagName, "PropInt");
        }

        [Fact]
        public void HtmlTagFor_Input_Value_Int()
        {
            var model = new Model1() { PropInt = 1 };
            var helper = new HtmlTagTModelHelper<Model1>(model);

            var propExpression = Expr((Model1 m) => m.PropInt);
            var tagName = "input";
            var tag = helper.HtmlTagFor(propExpression, tagName);

            AssertValid(tag, tagName, "PropInt", value: model.PropInt);
        }

        [Fact]
        public void HtmlTagFor_Input_Value_Date()
        {
            var model = new Model1() { PropDate = DateTime.UtcNow };
            var helper = new HtmlTagTModelHelper<Model1>(model);

            var propExpression = Expr((Model1 m) => m.PropDate);
            var tagName = "input";
            var tag = helper.HtmlTagFor(propExpression, tagName);

            AssertValid(tag, tagName, "PropDate", value: model.PropDate);
        }

        [Fact]
        public void HtmlTagFor_Input_Value_Null()
        {
            var model = new Model1() { PropInt = null };
            var helper = new HtmlTagTModelHelper<Model1>(model);

            var propExpression = Expr((Model1 m) => m.PropInt);
            var tagName = "input";
            var tag = helper.HtmlTagFor(propExpression, tagName);

            AssertValid(tag, tagName, "PropInt", value: model.PropInt);
        }

        [Fact]
        public void HtmlTagFor_Expression_MethodCall()
        {
            var helper = new HtmlTagTModelHelper<Model1>();
            Assert.Throws<InvalidOperationException>(
                () => helper.HtmlTagFor(m => m.PropInt.ToString(), "input"));
        }

        [Fact]
        public void HtmlTagFor_Expression_Constant()
        {
            var helper = new HtmlTagTModelHelper<Model1>();
            Assert.Throws<InvalidOperationException>(
                () => helper.HtmlTagFor(m => 1, "input"));
        }

        [Fact]
        public void TextAreaFor_Value_Int()
        {
            var model = new Model1() { PropInt = 1 };
            var helper = new HtmlTagTModelHelper<Model1>(model);

            var propExpression = Expr((Model1 m) => m.PropInt);
            var tag = helper.TextAreaFor(propExpression);

            AssertValid(tag, "textarea", "PropInt", value: model.PropInt);
        }

        [Fact]
        public void Input_Text_InputType_Value_Int()
        {
            var model = new Model1() { PropInt = 1 };
            var helper = new HtmlTagTModelHelper<Model1>(model);

            var propExpression = Expr((Model1 m) => m.PropInt);
            var type = InputType.Text;
            var tag = helper.InputFor(propExpression, type);

            AssertValid(tag, "input", "PropInt", type: "text", value: model.PropInt);
        }

        [Fact]
        public void Input_Text_Value_Int()
        {
            var model = new Model1() { PropInt = 1 };
            var helper = new HtmlTagTModelHelper<Model1>(model);

            var propExpression = Expr((Model1 m) => m.PropInt);
            var type = "text";
            var tag = helper.InputFor(propExpression, type);

            AssertValid(tag, "input", "PropInt", type: type, value: model.PropInt);
        }

        [Fact]
        public void Input_Any_Value_Int()
        {
            var model = new Model1() { PropInt = 1 };
            var helper = new HtmlTagTModelHelper<Model1>(model);

            var propExpression = Expr((Model1 m) => m.PropInt);
            var type = "any";
            var tag = helper.InputFor(propExpression, type);

            AssertValid(tag, "input", "PropInt", type: type, value: model.PropInt);
        }

        [Fact]
        public void TextBoxFor_Value_Int()
        {
            var model = new Model1() { PropInt = 1 };
            var helper = new HtmlTagTModelHelper<Model1>(model);

            var propExpression = Expr((Model1 m) => m.PropInt);
            var tag = helper.TextBoxFor(propExpression);

            AssertValid(tag, "input", "PropInt", type: "text", value: model.PropInt);
        }

        [Fact]
        public void PasswordFor_Value_Int()
        {
            var model = new Model1() { PropInt = 1 };
            var helper = new HtmlTagTModelHelper<Model1>(model);

            var propExpression = Expr((Model1 m) => m.PropInt);
            var tag = helper.PasswordFor(propExpression);

            AssertValid(tag, "input", "PropInt", type: "password", value: model.PropInt);
        }

        [Fact]
        public void HiddenFor_Value_Int()
        {
            var model = new Model1() { PropInt = 1 };
            var helper = new HtmlTagTModelHelper<Model1>(model);

            var propExpression = Expr((Model1 m) => m.PropInt);
            var tag = helper.HiddenFor(propExpression);

            AssertValid(tag, "input", "PropInt", type: "hidden", value: model.PropInt);
        }

        [Fact]
        public void RadioFor_Value_Int()
        {
            var model = new Model1() { PropInt = 1 };
            var helper = new HtmlTagTModelHelper<Model1>(model);

            var propExpression = Expr((Model1 m) => m.PropInt);
            var tag = helper.RadioFor(propExpression);

            AssertValid(tag, "input", "PropInt", type: "radio", value: model.PropInt);
        }

        [Fact]
        public void CheckBoxFor_Value_Int()
        {
            var model = new Model1() { PropInt = 1 };
            var helper = new HtmlTagTModelHelper<Model1>(model);

            var propExpression = Expr((Model1 m) => m.PropInt);
            var tag = helper.CheckBoxFor(propExpression);

            AssertValid(tag, "input", "PropInt", type: "checkbox", value: model.PropInt);
        }

        [Fact]
        public void CheckBoxFor_Value_Bool()
        {
            var model = new Model1() { PropBool = true };
            var helper = new HtmlTagTModelHelper<Model1>(model);

            var propExpression = Expr((Model1 m) => m.PropBool);
            var tag = helper.CheckBoxFor(propExpression);

            AssertValid(tag, "input", "PropBool", type: "checkbox", value: model.PropBool);
            Assert.False(tag.HasAttr("checked"));
        }

        [Fact]
        public void CheckBoxFor_Value_Bool_Checked()
        {
            var model = new Model1() { PropBool = true };
            var helper = new HtmlTagTModelHelper<Model1>(model);

            var propExpression = Expr((Model1 m) => m.PropBool);
            var tag = helper.CheckBoxFor(propExpression, isChecked: true);

            AssertValid(tag, "input", "PropBool", type: "checkbox", value: model.PropBool);
            Assert.Equal("true", tag.Attr("checked"));
        }
    }
}
