using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Bardock.Utils.Web.Mvc.ModelBinders
{
    /// <summary>
    /// Initializes model fields with specified default values when they were not specified in request params
    /// </summary>
    /// <typeparam name="T">Model type</typeparam>
    public class DefaultForMissingValueModelBinder<T> : DefaultValueModelBinder<T>
    {
        /// <summary>
        /// Determines if specified field will be initialized with default value.
        /// </summary>
        /// <param name="model">Model object</param>
        /// <param name="field">KeyValuePair with field name and default value</param>
        /// <param name="controllerContext">Controller context</param>
        /// <param name="bindingContext">Binding context</param>
        protected override bool RequiresDefaultValue(T model, KeyValuePair<string, Func<object>> field, ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var val = bindingContext.ValueProvider.GetValue(field.Key);
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