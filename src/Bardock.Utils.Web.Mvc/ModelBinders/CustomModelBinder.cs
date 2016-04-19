using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Bardock.Utils.Web.Mvc.ModelBinders
{
    /// <summary>
    /// Initializes model fields with specified values
    /// </summary>
    /// <typeparam name="T">Model type</typeparam>
    public class CustomModelBinder<T> : DefaultModelBinder
    {
        private List<FieldSpecification> _fields = new List<FieldSpecification>();

        public CustomModelBinder()
        { }

        /// <summary>
        /// Register a field to be initialized with specified value
        /// </summary>
        /// <typeparam name="TField"></typeparam>
        /// <param name="fieldExpr">Field expression</param>
        /// <param name="value">Constant default value</param>
        /// <param name="when">Additional condition that the field must satisfy in order to be populated with the default value</param>
        public CustomModelBinder<T> AddField<TField>(
            Expression<Func<T, TField>> fieldExpr,
            object value,
            Func<ValueProviderResult, object, bool> when = null)
        {
            return this.AddField(fieldExpr, () => value, when);
        }

        /// <summary>
        /// Register a field to be initialized with specified default value resolver
        /// </summary>
        /// <typeparam name="TField"></typeparam>
        /// <param name="fieldExpr">Field expression</param>
        /// <param name="valueResolver">Function that resolves default value. Useful when it depends on some context status</param>
        /// <param name="when">Additional condition that the field must satisfy in order to be populated with the default value</param>
        public CustomModelBinder<T> AddField<TField>(
            Expression<Func<T, TField>> fieldExpr,
            Func<object> valueResolver,
            Func<ValueProviderResult, object, bool> when = null)
        {
            _fields.Add(new FieldSpecification(
                name: Bardock.Utils.Linq.Expressions.ExpressionHelper.GetExpressionText(fieldExpr),
                valueResolver: valueResolver,
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
                if (EvaluateField(field, model, controllerContext, bindingContext))
                {
                    var prop = model.GetType().GetProperty(field.Name);
                    prop.GetSetMethod().Invoke(model, new object[] { field.ValueResolver() });
                }
            }
            return model;
        }

        /// <summary>
        /// Determines if specified field will be initialized with specified value.
        /// </summary>
        /// <param name="field">Field specification</param>
        /// <param name="model">Binded model object</param>
        /// <param name="controllerContext">Controller context</param>
        /// <param name="bindingContext">Binding context</param>
        protected virtual bool EvaluateField(FieldSpecification field, T model, ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(field.Name);

            var prop = model.GetType().GetProperty(field.Name);
            var bindedValue = prop.GetGetMethod().Invoke(model, null);

            return field.Condition(valueProviderResult, bindedValue);
        }

        /// <summary>
        /// Register this binder in specified ModelBinderDictionary
        /// </summary>
        public static CustomModelBinder<T> Register(ModelBinderDictionary binders)
        {
            var binder = new CustomModelBinder<T>();
            binders.Add(typeof(T), binder);
            return binder;
        }
    }

    public class FieldSpecification
    {
        public string Name { get; private set; }

        public Func<object> ValueResolver { get; private set; }

        public Func<ValueProviderResult, object, bool> Condition { get; private set; }

        public FieldSpecification(string name, Func<object> valueResolver, Func<ValueProviderResult, object, bool> condition = null)
        {
            this.Name = name;
            this.ValueResolver = valueResolver;
            this.Condition = condition ?? ((vpr, bindedValue) => true);
        }
    }
}