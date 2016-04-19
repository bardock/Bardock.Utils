using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Bardock.Utils.Web.Mvc.ModelBinders
{
    /// <summary>
    /// Initializes model fields with specified values
    /// </summary>
    /// <typeparam name="TModel">Model type</typeparam>
    public class CustomModelBinder<TModel> : DefaultModelBinder
    {
        private List<FieldSpecification<TModel>> _fields = new List<FieldSpecification<TModel>>();

        public CustomModelBinder()
        { }

        /// <summary>
        /// Register a field to be initialized with specified value
        /// </summary>
        /// <typeparam name="TField"></typeparam>
        /// <param name="fieldExpr">Field expression</param>
        /// <param name="value">Constant default value</param>
        /// <param name="when">Additional condition that the field must satisfy in order to be populated with the default value</param>
        public CustomModelBinder<TModel> AddField<TField>(
            Expression<Func<TModel, TField>> fieldExpr,
            object value,
            Func<FieldBindingContext<TModel>, bool> when = null)
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
        public CustomModelBinder<TModel> AddField<TField>(
            Expression<Func<TModel, TField>> fieldExpr,
            Func<object> valueResolver,
            Func<FieldBindingContext<TModel>, bool> when = null)
        {
            _fields.Add(new FieldSpecification<TModel>(
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
            TModel model = (TModel)base.BindModel(controllerContext, bindingContext);
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
        /// <param name="modelBindingContext">Binding context</param>
        protected virtual bool EvaluateField(
            FieldSpecification<TModel> field,
            TModel model,
            ControllerContext controllerContext,
            ModelBindingContext modelBindingContext)
        {
            var valueProviderResult = modelBindingContext.ValueProvider.GetValue(field.Name);

            var context = new FieldBindingContext<TModel>(
                valueProviderResult,
                model,
                controllerContext,
                modelBindingContext);

            return field.Condition(context);
        }

        /// <summary>
        /// Register this binder in specified ModelBinderDictionary
        /// </summary>
        public static CustomModelBinder<TModel> Register(ModelBinderDictionary binders)
        {
            var binder = new CustomModelBinder<TModel>();
            binders.Add(typeof(TModel), binder);
            return binder;
        }
    }

    public class FieldSpecification<TModel>
    {
        public string Name { get; private set; }

        public Func<object> ValueResolver { get; private set; }

        public Func<FieldBindingContext<TModel>, bool> Condition { get; private set; }

        public FieldSpecification(string name, Func<object> valueResolver, Func<FieldBindingContext<TModel>, bool> condition = null)
        {
            this.Name = name;
            this.ValueResolver = valueResolver;
            this.Condition = condition ?? (ctx => true);
        }
    }

    /// <summary>
    /// Represents the context while a field is being bound
    /// </summary>
    public class FieldBindingContext<TModel>
    {
        public ValueProviderResult Result { get; private set; }
        public TModel Model { get; private set; }
        public ControllerContext ControllerContext { get; private set; }
        public ModelBindingContext ModelBindingContext { get; private set; }

        /// <summary>
        /// Field was not specified in the binding context, regardless of its value.
        /// Returns false if field was specified with an empty, null or another value.
        /// </summary>
        public bool IsUndefinded { get { return Result == null; } }

        public FieldBindingContext(
            ValueProviderResult result,
            TModel model,
            ControllerContext controllerContext,
            ModelBindingContext modelBindingContext)
        {
            this.Result = result;
            this.Model = model;
            this.ControllerContext = controllerContext;
            this.ModelBindingContext = modelBindingContext;
        }
    }
}