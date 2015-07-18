using Bardock.Utils.Extensions;
using Bardock.Utils.Types;
using FluentAssertions;
using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Xunit;

namespace Bardock.Utils.Tests.Extensions
{
    public class MemberExpressionExtensionsTest
    {
        public class Dummy
        {
            public string Prop { get; set; }
            public string Field;
        }

        [Fact]
        public void ToExpression_FromPropertyInfo_ShouldBeMemberExpressionOfSameProp()
        {
            //Setup
            var prop = typeof(Dummy).GetProperty("Prop");

            //Exercise
            var actual = prop.ToExpression();

            //Verify
            actual.Should().NotBeNull().And.BeAssignableTo<MemberExpression>()
                .Which.Member.Should().BeSameAs(prop);
        }

        [Fact]
        public void ToExpression_FromNullPropertyInfo_ShouldThrowArgumentException()
        {
            //Setup
            PropertyInfo prop = null;

            //Exercise
            Action act = () => prop.ToExpression();

            //Verify
            act.ShouldThrow<ArgumentException>().WithMessage("prop is null");
        }

        [Fact]
        public void ToExpression_FromFieldInfo_ShouldBeMemberExpressionOfSameField()
        {
            //Setup
            var field = typeof(Dummy).GetField("Field");

            //Exercise
            var actual = field.ToExpression();

            //Verify
            actual.Should().NotBeNull().And.BeAssignableTo<MemberExpression>()
                .Which.Member.Should().BeSameAs(field);
        }

        [Fact]
        public void ToExpression_FromNullFieldInfo_ShouldThrowArgumentException()
        {
            //Setup
            FieldInfo field = null;

            //Exercise
            Action act = () => field.ToExpression();

            //Verify
            act.ShouldThrow<ArgumentException>().WithMessage("field is null");
        }
    }
}