using System.Collections.Generic;
using System.Linq;
using Bardock.Utils.Collections;
using Bardock.Utils.Globalization;
using Bardock.Utils.Web.Mvc.HtmlTags;
using Bardock.Utils.Web.Mvc.HtmlTags.Extensions;
using HtmlTags;
using Xunit;

namespace Bardock.Utils.Web.Mvc.SelectTags.Tests.Extensions
{
    public class SelectTagExtensionsTest
    {
        [Fact]
        public void PrependOption_Empty()
        {
            var tag = new SelectTag()
                .PrependOption("display0", 0);

            Assert.Equal(1, tag.Children.Count());

            var firstChild = tag.Children.First();

            Assert.Equal("display0", firstChild.Text());
            Assert.True(firstChild.ValueIsEqual(0));
        }

        [Fact]
        public void PrependOption_WithOption()
        {
            var tag = new SelectTag()
                .AddOption("display1", 1)
                .PrependOption("display0", 0);

            Assert.Equal(2, tag.Children.Count());

            var firstChild = tag.Children.First();
            var secondChild = tag.Children.Skip(1).First();

            Assert.Equal("display0", firstChild.Text());
            Assert.True(firstChild.ValueIsEqual(0));
            Assert.Equal("display1", secondChild.Text());
            Assert.True(secondChild.ValueIsEqual(1));
        }

        [Fact]
        public void AddOption_Empty()
        {
            var tag = new SelectTag()
                .AddOption("display0", 0);

            Assert.Equal(1, tag.Children.Count());

            var firstChild = tag.Children.First();

            Assert.Equal("display0", firstChild.Text());
            Assert.True(firstChild.ValueIsEqual(0));
        }

        [Fact]
        public void AddOption_WithOption()
        {
            var tag = new SelectTag()
                .AddOption("display0", 0)
                .AddOption("display1", 1);

            Assert.Equal(2, tag.Children.Count());

            var firstChild = tag.Children.First();
            var secondChild = tag.Children.Skip(1).First();

            Assert.Equal("display0", firstChild.Text());
            Assert.True(firstChild.ValueIsEqual(0));
            Assert.Equal("display1", secondChild.Text());
            Assert.True(secondChild.ValueIsEqual(1));
        }

        [Fact]
        public void SelectValue_Empty()
        {
            var tag = new SelectTag().SelectValue(1);

            Assert.Equal(0, tag.Children.Count());
        }

        [Fact]
        public void SelectValue_WithOption_UnexistingValue()
        {
            var tag = new SelectTag()
                .AddOption("display0", 0)
                .SelectValue(1);

            Assert.Equal(1, tag.Children.Count());

            var firstChild = tag.Children.First();

            Assert.Equal("display0", firstChild.Text());
            Assert.True(firstChild.ValueIsEqual(0));
            Assert.False(firstChild.HasAttr("selected"));
        }

        [Fact]
        public void SelectValue_WithOption()
        {
            var tag = new SelectTag()
                .AddOption("display0", 0)
                .SelectValue(0);

            Assert.Equal(1, tag.Children.Count());

            var firstChild = tag.Children.First();

            Assert.Equal("display0", firstChild.Text());
            Assert.True(firstChild.ValueIsEqual(0));
            Assert.True(firstChild.HasAttr("selected"));
        }

        [Fact]
        public void SelectValue_WithOptions()
        {
            var tag = new SelectTag()
                .AddOption("display0", 0)
                .AddOption("display1", 1)
                .SelectValue(0);

            Assert.Equal(2, tag.Children.Count());

            var firstChild = tag.Children.First();
            var secondChild = tag.Children.Skip(1).First();

            Assert.Equal("display0", firstChild.Text());
            Assert.True(firstChild.ValueIsEqual(0));
            Assert.True(firstChild.HasAttr("selected"));
            Assert.Equal("display1", secondChild.Text());
            Assert.True(secondChild.ValueIsEqual(1));
            Assert.False(secondChild.HasAttr("selected"));
        }

        [Fact]
        public void SelectValue_WithOptions_AnotherAlreadySelected()
        {
            var tag = new SelectTag()
                .AddOption("display0", 0)
                .AddOption("display1", 1)
                .SelectValue(1)
                .SelectValue(0);

            Assert.Equal(2, tag.Children.Count());

            var firstChild = tag.Children.First();
            var secondChild = tag.Children.Skip(1).First();

            Assert.Equal("display0", firstChild.Text());
            Assert.True(firstChild.ValueIsEqual(0));
            Assert.True(firstChild.HasAttr("selected"));
            Assert.Equal("display1", secondChild.Text());
            Assert.True(secondChild.ValueIsEqual(1));
            Assert.False(secondChild.HasAttr("selected"));
        }

        [Fact]
        public void SelectValue_WithOptions_SameAlreadySelected()
        {
            var tag = new SelectTag()
                .AddOption("display0", 0)
                .AddOption("display1", 1)
                .SelectValue(0)
                .SelectValue(0);

            Assert.Equal(2, tag.Children.Count());

            var firstChild = tag.Children.First();
            var secondChild = tag.Children.Skip(1).First();

            Assert.Equal("display0", firstChild.Text());
            Assert.True(firstChild.ValueIsEqual(0));
            Assert.True(firstChild.HasAttr("selected"));
            Assert.Equal("display1", secondChild.Text());
            Assert.True(secondChild.ValueIsEqual(1));
            Assert.False(secondChild.HasAttr("selected"));
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

            var tag = new SelectTag()
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

            Assert.Equal("display0", firstChild.Text());
            Assert.True(firstChild.ValueIsEqual(0));
            Assert.False(firstChild.HasAttr("selected"));
            Assert.Equal(0, firstChild.Data("customdata"));
            Assert.Equal("display1", secondChild.Text());
            Assert.True(secondChild.ValueIsEqual(1));
            Assert.True(secondChild.HasAttr("selected"));
            Assert.Equal(1, secondChild.Data("customdata"));
            Assert.Equal("display2", thirdChild.Text());
            Assert.True(thirdChild.ValueIsEqual(2));
            Assert.False(thirdChild.HasAttr("selected"));
            Assert.Equal(2, thirdChild.Data("customdata"));
        }

        [Fact]
        public void AddOptions_CustomItems_Empty()
        {
            var items = new List<CustomItem>();

            var tag = new SelectTag()
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
            var tag = new SelectTag()
                .AddOptions(
                    OptionsList.CreateForEnum<Enum1>(
                        isSelected: x => x.Value == 2,
                        configure: (x, op) => op.Data("customdata", x.Value)));

            Assert.Equal(2, tag.Children.Count());

            var firstChild = tag.Children.First();
            var secondChild = tag.Children.Skip(1).First();

            Assert.Equal("Option1", firstChild.Text());
            Assert.True(firstChild.ValueIsEqual(1));
            Assert.False(firstChild.HasAttr("selected"));
            Assert.Equal(1, firstChild.Data("customdata"));
            Assert.Equal("Option2", secondChild.Text());
            Assert.True(secondChild.ValueIsEqual(2));
            Assert.True(secondChild.HasAttr("selected"));
            Assert.Equal(2, secondChild.Data("customdata"));
        }

        [Fact]
        public void AddOptions_Enum_Display()
        {
            var tag = new SelectTag()
                .AddOptions(
                    OptionsList.CreateForEnum<Enum1>(
                        isSelected: x => x.Value == 2,
                        display: x => x.Name + "-" + x.Value,
                        configure: (x, op) => op.Data("customdata", x.Value)));

            Assert.Equal(2, tag.Children.Count());

            var firstChild = tag.Children.First();
            var secondChild = tag.Children.Skip(1).First();

            Assert.Equal("Option1-1", firstChild.Text());
            Assert.True(firstChild.ValueIsEqual(1));
            Assert.False(firstChild.HasAttr("selected"));
            Assert.Equal(1, firstChild.Data("customdata"));
            Assert.Equal("Option2-2", secondChild.Text());
            Assert.True(secondChild.ValueIsEqual(2));
            Assert.True(secondChild.HasAttr("selected"));
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

                var tag = new SelectTag()
                    .AddOptions(
                        OptionsList.CreateForEnum<Enum1>(
                            isSelected: x => x.Value == 2,
                            configure: (x, op) => op.Data("customdata", x.Value)));

                Assert.Equal(2, tag.Children.Count());

                var firstChild = tag.Children.First();
                var secondChild = tag.Children.Skip(1).First();

                Assert.Equal("Option 1", firstChild.Text());
                Assert.True(firstChild.ValueIsEqual(1));
                Assert.False(firstChild.HasAttr("selected"));
                Assert.Equal(1, firstChild.Data("customdata"));
                Assert.Equal("Option2", secondChild.Text());
                Assert.True(secondChild.ValueIsEqual(2));
                Assert.True(secondChild.HasAttr("selected"));
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
            var tag = new SelectTag()
                .AddOptions(
                    OptionsList.CreateForEnum<Enum1>(
                        selectedValue: 2,
                        configure: (x, op) => op.Data("customdata", x.Value)));

            Assert.Equal(2, tag.Children.Count());

            var firstChild = tag.Children.First();
            var secondChild = tag.Children.Skip(1).First();

            Assert.Equal("Option1", firstChild.Text());
            Assert.True(firstChild.ValueIsEqual(1));
            Assert.False(firstChild.HasAttr("selected"));
            Assert.Equal(1, firstChild.Data("customdata"));
            Assert.Equal("Option2", secondChild.Text());
            Assert.True(secondChild.ValueIsEqual(2));
            Assert.True(secondChild.HasAttr("selected"));
            Assert.Equal(2, secondChild.Data("customdata"));
        }

        [Fact]
        public void AddOptions_Enum_SelectedValues()
        {
            var tag = new SelectTag()
                .AddOptions(
                    OptionsList.CreateForEnum<Enum1>(
                        selectedValues: Coll.Array(2),
                        configure: (x, op) => op.Data("customdata", x.Value)));

            Assert.Equal(2, tag.Children.Count());

            var firstChild = tag.Children.First();
            var secondChild = tag.Children.Skip(1).First();

            Assert.Equal("Option1", firstChild.Text());
            Assert.True(firstChild.ValueIsEqual(1));
            Assert.False(firstChild.HasAttr("selected"));
            Assert.Equal(1, firstChild.Data("customdata"));
            Assert.Equal("Option2", secondChild.Text());
            Assert.True(secondChild.ValueIsEqual(2));
            Assert.True(secondChild.HasAttr("selected"));
            Assert.Equal(2, secondChild.Data("customdata"));
        }
    }
}
