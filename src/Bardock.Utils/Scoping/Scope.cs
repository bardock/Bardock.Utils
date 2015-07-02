using Bardock.Utils.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Bardock.Utils.Scoping
{
    public class Scope<T> : IDisposable
    {
        private T _instance;

        private IDictionary<Expression<Func<T, object>>, object> _values;

        public Scope(T instance, Action<Builder> config)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            if (config == null)
                throw new ArgumentNullException("config");

            var builder = new Builder();

            config(builder);

            _instance = instance;
            _values = new Dictionary<Expression<Func<T, object>>, object>();

            Init(builder);
        }

        public virtual void Dispose()
        {
            foreach (var p in _values)
                SetPropertyValue(p.Key, p.Value);
        }

        private void Init(Builder builder)
        {
            var scopeConfig = builder.Build();
            foreach (var p in scopeConfig)
            {
                _values.Add(p.Key, p.Key.Compile().Invoke(_instance));
                SetPropertyValue(p.Key, p.Value);
            }
        }

        private void SetPropertyValue(Expression<Func<T, object>> expr, object value)
        {
            var memberExpr = (MemberExpression)ExpressionHelper.RemoveConvert(expr).Body;

            var target = Expression.Lambda<Func<T, object>>(
                Expression.Convert(
                    memberExpr.Expression, typeof(object)),
                    expr.Parameters
                )
                .Compile()
                .Invoke(_instance);

            ((PropertyInfo)memberExpr.Member)
                .GetSetMethod()
                .Invoke(target, new object[] { value });
        }

        public class Builder
        {
            private IDictionary<Expression<Func<T, object>>, object> _mappings;

            public Builder()
            {
                _mappings = new Dictionary<Expression<Func<T, object>>, object>();
            }

            public Builder Set<TReturn>(Expression<Func<T, TReturn>> expr, TReturn value)
            {
                _mappings.Add(
                    Expression.Lambda<Func<T, object>>(Expression.Convert(expr.Body, typeof(object)), expr.Parameters),
                    (object)value);

                return this;
            }

            internal IDictionary<Expression<Func<T, object>>, object> Build()
            {
                return _mappings.ToDictionary(x => x.Key, x => x.Value);
            }
        }
    }

    public class Scope
    {
        public static Scope<T> Create<T>(T instance, Action<Scope<T>.Builder> config)
        {
            return new Scope<T>(instance, config);
        }
    }
}