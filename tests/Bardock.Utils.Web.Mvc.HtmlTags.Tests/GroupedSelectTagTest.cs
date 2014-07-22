using System;
using System.Collections.Generic;
using System.Linq;
using Bardock.Utils.Collections;
using Bardock.Utils.Globalization;
using Bardock.Utils.Web.Mvc.HtmlTags.Extensions;
using HtmlTags;
using Xunit;

namespace Bardock.Utils.Web.Mvc.HtmlTags.Tests
{
    public class GroupedSelectTagTest
    {
        private void AssertOptionGroups(
            SelectTag tag, 
            int groupsCount)
        {
            Assert.Equal(groupsCount, tag.Children.Count());
            Assert.Equal(groupsCount, tag.OptionGroups.Count());
            Assert.True(tag.OptionGroups.Values.All(g => tag.Children.Any(c => c == g)));
            Assert.Equal(tag.Children.SelectMany(x => x.Children).Count(), tag.Options.Count());
            Assert.True(tag.Options.All(g => tag.Children.SelectMany(x => x.Children).Any(c => c == g)));
        }

        private void AssertOption(
            HtmlTag option,
            string display, 
            object value)
        {
            Assert.Equal("option", option.TagName());
            Assert.Equal(display, option.Text());
            Assert.True(option.ValueIsEqual(value));
        }

        private void AssertOptionGroup(
            HtmlTag optgroup,
            string display)
        {
            Assert.Equal("optgroup", optgroup.TagName());
            Assert.Equal(display, optgroup.Attr("label"));
        }

        [Fact]
        public void PrependOption_Empty()
        {
            var tag = new SelectTag()
                .PrependOption("display0", 0, groupBy: "g0");

            AssertOptionGroups(tag, 1);
            var optgroup0 = tag.Children.First();
            AssertOptionGroup(optgroup0, "g0");

            Assert.Equal(1, optgroup0.Children.Count());
            var option0 = optgroup0.Children.First();
            AssertOption(option0, "display0", 0);
        }

        [Fact]
        public void PrependOption_WithOption()
        {
            var tag = new SelectTag()
                .AddOption("display1", 1, groupBy: "g0")
                .PrependOption("display0", 0, groupBy: "g0");

            AssertOptionGroups(tag, 1);
            var optgroup0 = tag.Children.First();
            AssertOptionGroup(optgroup0, "g0");

            Assert.Equal(2, optgroup0.Children.Count());

            var option0 = optgroup0.Children.Skip(0).First();
            AssertOption(option0, "display0", 0);

            var option1 = optgroup0.Children.Skip(1).First();
            AssertOption(option1, "display1", 1);
        }

        [Fact]
        public void PrependOption_WithOption_MultipleGroups()
        {
            var tag = new SelectTag()
                .AddOption("display1", 1, groupBy: "g0")
                .PrependOption("display0", 0, groupBy: "g0")
                .AddOption("display2", 2, groupBy: "g1");

            AssertOptionGroups(tag, 2);

            var optgroup0 = tag.Children.First();
            AssertOptionGroup(optgroup0, "g0");

            Assert.Equal(2, optgroup0.Children.Count());
            var option0 = optgroup0.Children.Skip(0).First();
            AssertOption(option0, "display0", 0);
            var option1 = optgroup0.Children.Skip(1).First();
            AssertOption(option1, "display1", 1);

            var optgroup1 = tag.Children.Skip(1).First();
            AssertOptionGroup(optgroup1, "g1");

            Assert.Equal(1, optgroup1.Children.Count());
            var option2 = optgroup1.Children.Skip(0).First();
            AssertOption(option2, "display2", 2);
        }

        [Fact]
        public void AddOption_Empty()
        {
            var tag = new SelectTag()
                .AddOption("display0", 0, groupBy: "g0");

            AssertOptionGroups(tag, 1);
            var optgroup0 = tag.Children.First();
            AssertOptionGroup(optgroup0, "g0");

            Assert.Equal(1, optgroup0.Children.Count());
            var option0 = optgroup0.Children.Skip(0).First();
            AssertOption(option0, "display0", 0);
        }

        [Fact]
        public void AddOption_WithOption()
        {
            var tag = new SelectTag()
                .AddOption("display0", 0, groupBy: "g0")
                .AddOption("display1", 1, groupBy: "g0");

            AssertOptionGroups(tag, 1);
            var optgroup0 = tag.Children.First();
            AssertOptionGroup(optgroup0, "g0");

            Assert.Equal(2, optgroup0.Children.Count());

            var option0 = optgroup0.Children.Skip(0).First();
            AssertOption(option0, "display0", 0);

            var option1 = optgroup0.Children.Skip(1).First();
            AssertOption(option1, "display1", 1);
        }

        [Fact]
        public void AddOption_WithOption_MultipleGroups()
        {
            var tag = new SelectTag()
                .AddOption("display0", 0, groupBy: "g0")
                .AddOption("display1", 1, groupBy: "g0")
                .AddOption("display2", 2, groupBy: "g1");

            AssertOptionGroups(tag, 2);

            var optgroup0 = tag.Children.First();
            AssertOptionGroup(optgroup0, "g0");

            Assert.Equal(2, optgroup0.Children.Count());
            var option0 = optgroup0.Children.Skip(0).First();
            AssertOption(option0, "display0", 0);
            var option1 = optgroup0.Children.Skip(1).First();
            AssertOption(option1, "display1", 1);

            var optgroup1 = tag.Children.Skip(1).First();
            AssertOptionGroup(optgroup1, "g1");

            Assert.Equal(1, optgroup1.Children.Count());
            var option2 = optgroup1.Children.Skip(0).First();
            AssertOption(option2, "display2", 2);
        }

        [Fact]
        public void SelectValue_WithOption_UnexistingValue()
        {
            var tag = new SelectTag()
                .AddOption("display0", 0, groupBy: "g0")
                .SelectValue(1);

            Assert.False(tag.Children.First().Children.First().HasAttr("selected"));
        }

        [Fact]
        public void SelectValue_WithOption()
        {
            var tag = new SelectTag()
                .AddOption("display0", 0, groupBy: "g0")
                .SelectValue(0);

            Assert.True(tag.Children.First().Children.First().HasAttr("selected"));
        }

        [Fact]
        public void SelectValue_WithOptions()
        {
            var tag = new SelectTag()
                .AddOption("display0", 0, groupBy: "g0")
                .AddOption("display1", 1, groupBy: "g0")
                .SelectValue(0);

            Assert.True(tag.Children.First().Children.First().HasAttr("selected"));
            Assert.False(tag.Children.First().Children.Skip(1).First().HasAttr("selected"));
        }
    }
}
