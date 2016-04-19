using System;
using System.Web.Mvc;

namespace Bardock.Utils.Web.Mvc.ModelBinders
{
    /// <summary>
    /// Initializes model fields with specified default values when they were not specified in request params
    /// </summary>
    /// <typeparam name="TModel">Model type</typeparam>
    [Obsolete("Please use a CustomModelBinder with a condition like this: ctx => !ctx.Result.IsPresent()")]
    public class DefaultForMissingValueModelBinder<TModel> : DefaultValueModelBinder<TModel>
    {
        /// <summary>
        /// Determines if specified field will be initialized with default value.
        /// Returns true when field is not present in the binding context.
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="model">Binded model object</param>
        /// <param name="controllerContext">Controller context</param>
        /// <param name="bindingContext">Binding context</param>
        protected override bool FieldIsEmpty(string fieldName, TModel model, ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var val = bindingContext.ValueProvider.GetValue(fieldName);
            return val == null;
        }

        /// <summary>
        /// Register this binder in specified ModelBinderDictionary
        /// </summary>
        public static DefaultForMissingValueModelBinder<TModel> Register(ModelBinderDictionary binders)
        {
            var binder = new DefaultForMissingValueModelBinder<TModel>();
            binders.Add(typeof(TModel), binder);
            return binder;
        }
    }
}