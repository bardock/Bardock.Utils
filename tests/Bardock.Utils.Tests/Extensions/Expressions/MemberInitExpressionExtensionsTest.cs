using System;
using System.Linq;
using System.Linq.Expressions;
using Bardock.Utils.Extensions.Expressions;
using Xunit;

namespace Bardock.Utils.Tests.Extensions
{
    public class MemberInitExpressionExtensionsTest
    {
        /// <summary>
        /// Provides type inference when declaring a new expression
        /// </summary>
        private Expression<Func<T, TResult>> Expr<T, TResult>(Expression<Func<T, TResult>> exp)
        {
            return exp;
        }

        private class Type1 { }

        private class Type2 { public int Prop1 { get; set; } }

        private class Type3 : Type2 { public int Prop2 { get; set; } }

        [Fact]
        public void Do_Not_Repeat_Members()
        {
            const int PROP1_VALUE = 2;
            var exp1 = Expr((Type1 x) => new Type3 { Prop1 = PROP1_VALUE });
            var exp3 = Expr((Type1 x) => new Type3 { Prop2 = 3 });

            var result = exp1.AddMembers(exp3);

            AssertValid(result, prop1Value: PROP1_VALUE);
        }

        [Fact]
        public void Repeat_Members()
        {
            const int PROP1_VALUE = 1;
            var exp1 = Expr((Type1 x) => new Type3 { Prop1 = 2 });
            var exp3 = Expr((Type1 x) => new Type3 { Prop1 = PROP1_VALUE, Prop2 = 3 });

            var result = exp1.AddMembers(exp3);

            AssertValid(result, prop1Value: PROP1_VALUE);
        }

        [Fact]
        public void Use_SubClass_Do_Not_Repeat_Members()
        {
            const int PROP1_VALUE = 2;
            var exp1 = Expr((Type1 x) => new Type2 { Prop1 = PROP1_VALUE });
            var exp3 = Expr((Type1 x) => new Type3 { Prop2 = 3 });

            var result = exp1.AddMembers(exp3);

            AssertValid(result, prop1Value: PROP1_VALUE);
        }

        [Fact]
        public void Use_SubClass_Repeat_Members()
        {
            const int PROP1_VALUE = 1;
            var exp1 = Expr((Type1 x) => new Type2 { Prop1 = 2 });
            var exp3 = Expr((Type1 x) => new Type3 { Prop1 = PROP1_VALUE, Prop2 = 3 });

            var result = exp1.AddMembers(exp3);

            AssertValid(result, prop1Value: PROP1_VALUE);
        }

        private void AssertValid(Expression<Func<Type1, Type3>> result, int prop1Value)
        {
            Assert.Equal(typeof(Type3), result.ReturnType);

            var memberInitExpr = result.Body as MemberInitExpression;
            Assert.NotNull(memberInitExpr);

            var bindings = memberInitExpr.Bindings.Cast<MemberAssignment>();
            Assert.Equal(2, bindings.Count());

            var prop1 = bindings.FirstOrDefault(b => b.Member == typeof(Type2).GetMember("Prop1").Last());
            Assert.NotNull(prop1);
            Assert.Equal(prop1Value, ((ConstantExpression)prop1.Expression).Value);

            var prop2 = bindings.FirstOrDefault(b => b.Member == typeof(Type3).GetMember("Prop2")[0]);
            Assert.NotNull(prop2);
            Assert.Equal(3, ((ConstantExpression)prop2.Expression).Value);
        }
    }
}