using System;
using System.Web.Mvc;

namespace Bardock.Utils.Web.Mvc.ModelBinders
{
    /// <summary>
    /// Initializes model fields with specified default values
    /// </summary>
    /// <typeparam name="T">Model type</typeparam>
    [Obsolete("Please use a CustomModelBinder with a condition like this: (_, bindedValue) => bindedValue == null")]
    public class DefaultValueModelBinder<T> : CustomModelBinder<T>
    {
        /// <summary>
        /// Determines if specified field will be initialized with specified value.
        /// </summary>
        /// <param name="field">Field specification</param>
        /// <param name="model">Binded model object</param>
        /// <param name="controllerContext">Controller context</param>
        /// <param name="bindingContext">Binding context</param>
        protected virtual bool EvaluateField(FieldSpecification field, T model, ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            return FieldIsEmpty(field.Name, model, controllerContext, bindingContext)
                && base.EvaluateField(field, model, controllerContext, bindingContext);
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
        public static DefaultValueModelBinder<T> Register(ModelBinderDictionary binders)
        {
            var binder = new DefaultValueModelBinder<T>();
            binders.Add(typeof(T), binder);
            return binder;
        }
    }
}