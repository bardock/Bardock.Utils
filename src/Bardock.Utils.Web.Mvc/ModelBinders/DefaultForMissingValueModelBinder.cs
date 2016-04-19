using System;
using System.Web.Mvc;

namespace Bardock.Utils.Web.Mvc.ModelBinders
{
    /// <summary>
    /// Initializes model fields with specified default values when they were not specified in request params
    /// </summary>
    /// <typeparam name="T">Model type</typeparam>
    [Obsolete("Please use a CustomModelBinder with a condition like this: (valueResultProvider, _) => !valueResultProvider.IsPresent()")]
    public class DefaultForMissingValueModelBinder<T> : DefaultValueModelBinder<T>
    {
        /// <summary>
        /// Determines if specified field will be initialized with default value.
        /// Returns true when field is not present in the binding context.
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="model">Binded model object</param>
        /// <param name="controllerContext">Controller context</param>
        /// <param name="bindingContext">Binding context</param>
        protected override bool FieldIsEmpty(string fieldName, T model, ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var val = bindingContext.ValueProvider.GetValue(fieldName);
            return val == null;
        }

        /// <summary>
        /// Register this binder in specified ModelBinderDictionary
        /// </summary>
        public static DefaultForMissingValueModelBinder<T> Register(ModelBinderDictionary binders)
        {
            var binder = new DefaultForMissingValueModelBinder<T>();
            binders.Add(typeof(T), binder);
            return binder;
        }
    }
}