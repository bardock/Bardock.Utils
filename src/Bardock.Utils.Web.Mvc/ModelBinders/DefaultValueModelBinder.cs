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
        private List<FieldSpecification> _fields = new List<FieldSpecification>();

        public DefaultValueModelBinder()
        { }

        /// <summary>
        /// Register a field to be initialized with specified default value
        /// </summary>
        /// <typeparam name="TField"></typeparam>
        /// <param name="fieldExpr">Field expression</param>
        /// <param name="defaultValue">Constant default value</param>
        /// <param name="when">Additional condition that the field must satisfy in order to be populated with the default value</param>
        public DefaultValueModelBinder<T> AddField<TField>(
            Expression<Func<T, TField>> fieldExpr,
            object defaultValue,
            Func<ValueProviderResult, bool> when = null)
        {
            return this.AddField(fieldExpr, () => defaultValue, when);
        }

        /// <summary>
        /// Register a field to be initialized with specified default value resolver
        /// </summary>
        /// <typeparam name="TField"></typeparam>
        /// <param name="fieldExpr">Field expression</param>
        /// <param name="defaultValueResolver">Function that resolves default value. Useful when it depends on some context status</param>
        /// <param name="when">Additional condition that the field must satisfy in order to be populated with the default value</param>
        public DefaultValueModelBinder<T> AddField<TField>(
            Expression<Func<T, TField>> fieldExpr,
            Func<object> defaultValueResolver,
            Func<ValueProviderResult, bool> when = null)
        {
            _fields.Add(new FieldSpecification(
                name: Bardock.Utils.Linq.Expressions.ExpressionHelper.GetExpressionText(fieldExpr),
                defaultValueResolver: defaultValueResolver,
                condition: when));
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
                var val = bindingContext.ValueProvider.GetValue(field.Name);
                if (FieldIsEmpty(field.Name, model, controllerContext, bindingContext) && field.Condition(val))
                {
                    var prop = model.GetType().GetProperty(field.Name);
                    prop.GetSetMethod().Invoke(model, new object[] { field.DefaultValueResolver() });
                }
            }
            return model;
        }

        /// <summary>
        /// Determines if specified field will be initialized with default value.
        /// Returns true when field was binded to a null value.
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="model">Binded model object</param>
        /// <param name="controllerContext">Controller context</param>
        /// <param name="bindingContext">Binding context</param>
        protected virtual bool FieldIsEmpty(string fieldName, T model, ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var prop = model.GetType().GetProperty(fieldName);
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

        private class FieldSpecification
        {
            public string Name { get; private set; }
            public Func<object> DefaultValueResolver { get; private set; }
            public Func<ValueProviderResult, bool> Condition { get; private set; }

            public FieldSpecification(string name, Func<object> defaultValueResolver, Func<ValueProviderResult, bool> condition = null)
            {
                this.Name = name;
                this.DefaultValueResolver = defaultValueResolver;
                this.Condition = condition ?? (r => true);
            }
        }
    }
}