using Bardock.Utils.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Bardock.Utils.Scoping
{
    public class Scope<T> : IDisposable
    {
        private T _instance;

        private IDictionary<string, Tuple<object, object>> _values;

        public Scope(T instance, Func<Builder, Builder> factoryFunc)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            if (factoryFunc == null)
                throw new ArgumentNullException("factoryFunc");

            var builder = factoryFunc(new Builder());
            if (builder == null)
                throw new NullReferenceException();

            _instance = instance;
            _values = new Dictionary<string, Tuple<object, object>>();

            var scopeConfig = builder.Build();
            foreach (var p in scopeConfig)
            {
                var propInfo = typeof(T).GetProperty(p.Key);
                _values.Add(p.Key, Tuple.Create(propInfo.GetValue(instance), p.Value));
                propInfo.SetValue(instance, p.Value);
            }
        }

        public virtual void Dispose()
        {
            foreach (var p in _values)
                typeof(T).GetProperty(p.Key).SetValue(_instance, p.Value.Item1);
        }

        public class Builder
        {
            private IDictionary<string, object> _mappings;

            public Builder()
            {
                _mappings = new Dictionary<string, object>();
            }

            public void AddValue<TReturn>(Expression<Func<T, TReturn>> expr, TReturn value)
            {
                _mappings.Add(ExpressionHelper.GetExpressionText(expr), (object)value);
            }

            public IDictionary<string, object> Build()
            {
                return _mappings.ToDictionary(x => x.Key, x => x.Value);
            }
        }
    }

    public class Scope
    {
        public static Scope<T> Create<T>(T instance, Func<Scope<T>.Builder, Scope<T>.Builder> factoryFunc)
        {
            return new Scope<T>(instance, factoryFunc);
        }
    }

    public static class ScopeBuilderExtensions
    {
        public static Scope<T>.Builder Add<T, TProperty>(this Scope<T>.Builder @this, Expression<Func<T, TProperty>> expr, TProperty value)
        {
            @this.AddValue(expr, value);
            return @this;
        }
    }
}