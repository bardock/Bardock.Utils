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

        private IDictionary<Expression<Func<T, object>>, object> _values;

        public Scope(T instance, Action<Builder> factoryFunc)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            if (factoryFunc == null)
                throw new ArgumentNullException("factoryFunc");


            var builder = new Builder();
            
            factoryFunc(builder);

            _instance = instance;
            _values = new Dictionary<Expression<Func<T,object>>, object>();

            var scopeConfig = builder.Build();
            foreach (var p in scopeConfig)
            {
                _values.Add(p.Key, p.Key.Compile().Invoke(instance));
                //propInfo.SetValue(instance, p.Value);
            }
        }

        public virtual void Dispose()
        {
            //foreach (var p in _values)
            //    typeof(T).GetProperty(p.Key).SetValue(_instance, p.Value);
        }

        public class Builder
        {
            private IDictionary<Expression<Func<T,object>>, object> _mappings;

            public Builder()
            {
                _mappings = new Dictionary<Expression<Func<T, object>>, object>();
            }

            public Builder Add<TReturn>(Expression<Func<T, TReturn>> expr, TReturn value)
            {
                //_mappings.Add(ExpressionHelper.GetExpressionText(expr), (object)value);
                return this;
            }

            public IDictionary<Expression<Func<T, object>>, object> Build()
            {
                return _mappings.ToDictionary(x => x.Key, x => x.Value);
            }
        }
    }

    public class Scope
    {
        public static Scope<T> Create<T>(T instance, Action<Scope<T>.Builder> factoryFunc)
        {
            return new Scope<T>(instance, factoryFunc);
        }
    }
}