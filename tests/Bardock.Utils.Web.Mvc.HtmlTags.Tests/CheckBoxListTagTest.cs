using System.Collections.Generic;
using System.Linq;
using Bardock.Utils.Collections;
using Bardock.Utils.Globalization;
using Bardock.Utils.Web.Mvc.HtmlTags.Extensions;
using HtmlTags;
using Xunit;

namespace Bardock.Utils.Web.Mvc.HtmlTags.Tests
{
    public class CheckBoxListTagTest
    {
        private const string DEFAULT_NAME = "name1";

        [Fact]
        public void CssClass()
        {
            var tag = new CheckBoxListTag(DEFAULT_NAME, cssClass: "class1");

            Assert.Equal(0, tag.Children.Count());
            Assert.True(tag.HasClass("class1"));
        }

        [Fact]
        public void CssClass_Default()
        {
            var tag = new CheckBoxListTag(DEFAULT_NAME);

            Assert.Equal(0, tag.Children.Count());
            Assert.True(tag.HasClass(CheckBoxListTag.DEFAULT_CSS_CLASS));
        }

        [Fact]
        public void CssClass_Null()
        {
            var tag = new CheckBoxListTag(DEFAULT_NAME, cssClass: null);

            Assert.Equal(0, tag.Children.Count());
            Assert.False(tag.HasClass(CheckBoxListTag.DEFAULT_CSS_CLASS));
        }

        [Fact]
        public void CssClass_Empty()
        {
            var tag = new CheckBoxListTag(DEFAULT_NAME, cssClass: "");

            Assert.Equal(0, tag.Children.Count());
            Assert.False(tag.HasClass(CheckBoxListTag.DEFAULT_CSS_CLASS));
        }

        public static void AssertValidChild(HtmlTag child, string name, string display, object value, bool isChecked = false)
        {
            Assert.Equal("label", child.TagName());
            Assert.Equal(display, child.Text());

            var input = child.FirstChild();
            Assert.Equal("input", input.TagName());
            Assert.Equal("checkbox", input.Attr("type"));
            Assert.Equal(name, input.Attr("name"));
            Assert.True(input.ValueIsEqual(value));
            Assert.Equal(isChecked, input.HasAttr("checked"));
        }

        [Fact]
        public void PrependOption_Empty()
        {
            var tag = new CheckBoxListTag(DEFAULT_NAME)
                .PrependOption("display0", 0);

            Assert.Equal(1, tag.Children.Count());
            AssertValidChild(tag.Children.First(), name: DEFAULT_NAME, display: "display0", value: 0);
        }

        [Fact]
        public void PrependOption_WithOption()
        {
            var tag = new CheckBoxListTag(DEFAULT_NAME)
                .AddOption("display1", 1)
                .PrependOption("display0", 0);

            Assert.Equal(2, tag.Children.Count());
            AssertValidChild(tag.Children.Skip(0).First(), name: DEFAULT_NAME, display: "display0", value: 0);
            AssertValidChild(tag.Children.Skip(1).First(), name: DEFAULT_NAME, display: "display1", value: 1);
        }

        [Fact]
        public void AddOption_Empty()
        {
            var tag = new CheckBoxListTag(DEFAULT_NAME)
                .AddOption("display0", 0);

            Assert.Equal(1, tag.Children.Count());
            AssertValidChild(tag.Children.Skip(0).First(), name: DEFAULT_NAME, display: "display0", value: 0);
        }

        [Fact]
        public void AddOption_WithOption()
        {
            var tag = new CheckBoxListTag(DEFAULT_NAME)
                .AddOption("display0", 0)
                .AddOption("display1", 1);

            Assert.Equal(2, tag.Children.Count());
            AssertValidChild(tag.Children.Skip(0).First(), name: DEFAULT_NAME, display: "display0", value: 0);
            AssertValidChild(tag.Children.Skip(1).First(), name: DEFAULT_NAME, display: "display1", value: 1);
        }

        [Fact]
        public void CheckValue_Empty()
        {
            var tag = new CheckBoxListTag(DEFAULT_NAME).CheckValue(1);

            Assert.Equal(0, tag.Children.Count());
        }

        [Fact]
        public void CheckValue_WithOption_UnexistingValue()
        {
            var tag = new CheckBoxListTag(DEFAULT_NAME)
                .AddOption("display0", 0)
                .CheckValue(1);

            Assert.Equal(1, tag.Children.Count());
            AssertValidChild(tag.Children.Skip(0).First(), name: DEFAULT_NAME, display: "display0", value: 0, isChecked: false);
        }

        [Fact]
        public void CheckValue_WithOption()
        {
            var tag = new CheckBoxListTag(DEFAULT_NAME)
                .AddOption("display0", 0)
                .CheckValue(0);

            Assert.Equal(1, tag.Children.Count());
            AssertValidChild(tag.Children.Skip(0).First(), name: DEFAULT_NAME, display: "display0", value: 0, isChecked: true);
        }

        [Fact]
        public void CheckValue_WithOptions()
        {
            var tag = new CheckBoxListTag(DEFAULT_NAME)
                .AddOption("display0", 0)
                .AddOption("display1", 1)
                .CheckValue(0);

            Assert.Equal(2, tag.Children.Count());
            AssertValidChild(tag.Children.Skip(0).First(), name: DEFAULT_NAME, display: "display0", value: 0, isChecked: true);
            AssertValidChild(tag.Children.Skip(1).First(), name: DEFAULT_NAME, display: "display1", value: 1, isChecked: false);
        }

        [Fact]
        public void CheckValue_WithOptions_AnotherAlreadySelected()
        {
            var tag = new CheckBoxListTag(DEFAULT_NAME)
                .AddOption("display0", 0)
                .AddOption("display1", 1)
                .CheckValue(1)
                .CheckValue(0);

            Assert.Equal(2, tag.Children.Count());
            AssertValidChild(tag.Children.Skip(0).First(), name: DEFAULT_NAME, display: "display0", value: 0, isChecked: true);
            AssertValidChild(tag.Children.Skip(1).First(), name: DEFAULT_NAME, display: "display1", value: 1, isChecked: true);
        }

        [Fact]
        public void CheckValue_WithOptions_SameAlreadySelected()
        {
            var tag = new CheckBoxListTag(DEFAULT_NAME)
                .AddOption("display0", 0)
                .AddOption("display1", 1)
                .CheckValue(0)
                .CheckValue(0);

            Assert.Equal(2, tag.Children.Count());
            AssertValidChild(tag.Children.Skip(0).First(), name: DEFAULT_NAME, display: "display0", value: 0, isChecked: true);
            AssertValidChild(tag.Children.Skip(1).First(), name: DEFAULT_NAME, display: "display1", value: 1, isChecked: false);
        }

        [Fact]
        public void CheckValues()
        {
            var tag = new CheckBoxListTag(DEFAULT_NAME)
                .AddOption("display0", 0)
                .AddOption("display1", 1)
                .AddOption("display2", 2)
                .CheckValues(1,2,3);

            Assert.Equal(3, tag.Children.Count());
            AssertValidChild(tag.Children.Skip(0).First(), name: DEFAULT_NAME, display: "display0", value: 0, isChecked: false);
            AssertValidChild(tag.Children.Skip(1).First(), name: DEFAULT_NAME, display: "display1", value: 1, isChecked: true);
            AssertValidChild(tag.Children.Skip(2).First(), name: DEFAULT_NAME, display: "display2", value: 2, isChecked: true);
        }

        private class CustomItem
        {
            public string Display { get; set; }
            public int Value { get; set; }
            public bool IsSelected { get; set; }
        }

        [Fact]
        public void AddOptions_CustomItems()
        {
            var items = new List<CustomItem>()
            {
                new CustomItem() { Display = "display0", Value = 0, IsSelected = false },
                new CustomItem() { Display = "display1", Value = 1, IsSelected = true },
                new CustomItem() { Display = "display2", Value = 2, IsSelected = false },
            };

            var tag = new CheckBoxListTag(DEFAULT_NAME)
                .AddOptions(
                    OptionsList.Create(
                        items,
                        display: x => x.Display,
                        value: x => x.Value,
                        isSelected: x => x.IsSelected,
                        configure: (x, op) => op.Data("customdata", x.Value)));

            Assert.Equal(3, tag.Children.Count());

            var firstChild = tag.Children.First();
            var secondChild = tag.Children.Skip(1).First();
            var thirdChild = tag.Children.Skip(2).First();

            AssertValidChild(firstChild, name: DEFAULT_NAME, display: "display0", value: 0, isChecked: false);
            AssertValidChild(secondChild, name: DEFAULT_NAME, display: "display1", value: 1, isChecked: true);
            AssertValidChild(thirdChild, name: DEFAULT_NAME, display: "display2", value: 2, isChecked: false);

            Assert.Equal(0, firstChild.Data("customdata"));
            Assert.Equal(1, secondChild.Data("customdata"));
            Assert.Equal(2, thirdChild.Data("customdata"));
        }

        [Fact]
        public void AddOptions_CustomItems_Empty()
        {
            var items = new List<CustomItem>();

            var tag = new CheckBoxListTag(DEFAULT_NAME)
                .AddOptions(
                    OptionsList.Create(
                        items,
                        display: x => x.Display,
                        value: x => x.Value,
                        isSelected: x => x.IsSelected,
                        configure: (x, op) => op.Data("customdata", x.Value)));

            Assert.Equal(0, tag.Children.Count());
        }

        private enum Enum1
        {
            Option1 = 1,
            Option2 = 2
        }

        [Fact]
        public void AddOptions_Enum()
        {
            var tag = new CheckBoxListTag(DEFAULT_NAME)
                .AddOptions(
                    OptionsList.CreateForEnum<Enum1>(
                        isSelected: x => x.Value == 2,
                        configure: (x, op) => op.Data("customdata", x.Value)));

            Assert.Equal(2, tag.Children.Count());

            var firstChild = tag.Children.First();
            var secondChild = tag.Children.Skip(1).First();

            AssertValidChild(firstChild, name: DEFAULT_NAME, display: "Option1", value: 1, isChecked: false);
            AssertValidChild(secondChild, name: DEFAULT_NAME, display: "Option2", value: 2, isChecked: true);

            Assert.Equal(1, firstChild.Data("customdata"));
            Assert.Equal(2, secondChild.Data("customdata"));
        }

        [Fact]
        public void AddOptions_Enum_Display()
        {
            var tag = new CheckBoxListTag(DEFAULT_NAME)
                .AddOptions(
                    OptionsList.CreateForEnum<Enum1>(
                        isSelected: x => x.Value == 2,
                        display: x => x.Name + "-" + x.Value,
                        configure: (x, op) => op.Data("customdata", x.Value)));

            Assert.Equal(2, tag.Children.Count());

            var firstChild = tag.Children.First();
            var secondChild = tag.Children.Skip(1).First();

            AssertValidChild(firstChild, name: DEFAULT_NAME, display: "Option1-1", value: 1, isChecked: false);
            AssertValidChild(secondChild, name: DEFAULT_NAME, display: "Option2-2", value: 2, isChecked: true);

            Assert.Equal(1, firstChild.Data("customdata"));
            Assert.Equal(2, secondChild.Data("customdata"));
        }

        public static class Enum1Resources
        {
            public static string Option1 { get { return "Option 1"; } }
        }

        [Fact]
        public void AddOptions_Enum_Localized()
        {
            var prevResource = Resources.Current;
            try
            {
                Resources.Register(new TypedClassResourceProvider(typeof(Enum1Resources)));

                var tag = new CheckBoxListTag(DEFAULT_NAME)
                    .AddOptions(
                        OptionsList.CreateForEnum<Enum1>(
                            isSelected: x => x.Value == 2,
                            configure: (x, op) => op.Data("customdata", x.Value)));

                Assert.Equal(2, tag.Children.Count());

                var firstChild = tag.Children.First();
                var secondChild = tag.Children.Skip(1).First();

                AssertValidChild(firstChild, name: DEFAULT_NAME, display: "Option 1", value: 1, isChecked: false);
                AssertValidChild(secondChild, name: DEFAULT_NAME, display: "Option2", value: 2, isChecked: true);

                Assert.Equal(1, firstChild.Data("customdata"));
                Assert.Equal(2, secondChild.Data("customdata"));
            }
            finally
            {
                Resources.Register(prevResource);
            }
        }

        [Fact]
        public void AddOptions_Enum_SelectedValue()
        {
            var tag = new CheckBoxListTag(DEFAULT_NAME)
                .AddOptions(
                    OptionsList.CreateForEnum<Enum1>(
                        selectedValue: 2,
                        configure: (x, op) => op.Data("customdata", x.Value)));

            Assert.Equal(2, tag.Children.Count());

            var firstChild = tag.Children.First();
            var secondChild = tag.Children.Skip(1).First();

            AssertValidChild(firstChild, name: DEFAULT_NAME, display: "Option1", value: 1, isChecked: false);
            AssertValidChild(secondChild, name: DEFAULT_NAME, display: "Option2", value: 2, isChecked: true);

            Assert.Equal(1, firstChild.Data("customdata"));
            Assert.Equal(2, secondChild.Data("customdata"));
        }

        [Fact]
        public void AddOptions_Enum_SelectedValues()
        {
            var tag = new CheckBoxListTag(DEFAULT_NAME)
                .AddOptions(
                    OptionsList.CreateForEnum<Enum1>(
                        selectedValues: Coll.Array(2),
                        configure: (x, op) => op.Data("customdata", x.Value)));

            Assert.Equal(2, tag.Children.Count());

            var firstChild = tag.Children.First();
            var secondChild = tag.Children.Skip(1).First();

            AssertValidChild(firstChild, name: DEFAULT_NAME, display: "Option1", value: 1, isChecked: false);
            AssertValidChild(secondChild, name: DEFAULT_NAME, display: "Option2", value: 2, isChecked: true);

            Assert.Equal(1, firstChild.Data("customdata"));
            Assert.Equal(2, secondChild.Data("customdata"));
        }
    }
}
