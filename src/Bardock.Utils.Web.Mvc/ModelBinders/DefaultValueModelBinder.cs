using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Bardock.Utils.Web.Mvc.ModelBinders
{
    /// <summary>
    /// Initializes model fields with specified default values
    /// </summary>
    /// <typeparam name="T">Model type</typeparam>
    public class DefaultValueModelBinder<T> : DefaultModelBinder
    {
        private Dictionary<string, Func<object>> _fields = new Dictionary<string, Func<object>>();

        public DefaultValueModelBinder()
        { }

        /// <summary>
        /// Register a field to be initialized with specified default value
        /// </summary>
        /// <typeparam name="TField"></typeparam>
        /// <param name="fieldExpr">Field expression</param>
        /// <param name="defaultValue">Constant default value</param>
        public DefaultValueModelBinder<T> AddField<TField>(Expression<Func<T, TField>> fieldExpr, object defaultValue)
        {
            return this.AddField(fieldExpr, () => defaultValue);
        }

        /// <summary>
        /// Register a field to be initialized with specified default value resolver
        /// </summary>
        /// <typeparam name="TField"></typeparam>
        /// <param name="fieldExpr">Field expression</param>
        /// <param name="defaultValueResolver">Function that resolves default value. Useful when it depends on some context status</param>
        public DefaultValueModelBinder<T> AddField<TField>(Expression<Func<T, TField>> fieldExpr, Func<object> defaultValueResolver)
        {
            _fields.Add(Bardock.Utils.Linq.Expressions.ExpressionHelper.GetExpressionText(fieldExpr), defaultValueResolver);
            return this;
        }

        /// <summary>
        /// Overrides base BindModel in order to set default values
        /// </summary>
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            T model = (T)base.BindModel(controllerContext, bindingContext);
            foreach (var field in _fields)
            {
                if (RequiresDefaultValue(model, field, controllerContext, bindingContext))
                {
                    var prop = model.GetType().GetProperty(field.Key);
                    prop.GetSetMethod().Invoke(model, new object[] { field.Value() });
                }
            }
            return model;
        }

        /// <summary>
        /// Determines if specified field will be initialized with default value
        /// </summary>
        /// <param name="model">Model object</param>
        /// <param name="field">KeyValuePair with field name and default value</param>
        /// <param name="controllerContext">Controller context</param>
        /// <param name="bindingContext">Binding context</param>
        protected virtual bool RequiresDefaultValue(T model, KeyValuePair<string, Func<object>> field, ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var prop = model.GetType().GetProperty(field.Key);
            var val = prop.GetGetMethod().Invoke(model, null);
            return val == null;
        }

        /// <summary>
        /// Register this binder in specified ModelBinderDictionary
        /// </summary>
        public static DefaultValueModelBinder<T> Register(ref ModelBinderDictionary binders)
        {
            var binder = new DefaultValueModelBinder<T>();
            binders.Add(typeof(T), binder);
            return binder;
        }
    }
}