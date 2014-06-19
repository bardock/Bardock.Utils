using Bardock.Utils.Web.Mvc.Helpers;
using bardock = Bardock.Utils.Linq.Expressions;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Bardock.Utils.Web.Mvc.ModelBinders
{
	public class InnerJsonModelBinder<T> : DefaultModelBinder
	{
		private string[] _fieldNames;
		public InnerJsonModelBinder(params string[] fieldNames)
		{
			_fieldNames = fieldNames;
		}

		public InnerJsonModelBinder(params Expression<Func<T, object>>[] fieldNameExp)
		{
			_fieldNames = fieldNameExp.Select(x => bardock.ExpressionHelper.GetExpressionText(x)).ToArray();
		}

		public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			var ret = base.BindModel(controllerContext, bindingContext);
			foreach (var fieldName in _fieldNames) {
				object val = bindingContext.ValueProvider.GetValue(fieldName);
				if ((val == null)) {
					continue;
				}
				var json = bindingContext.ValueProvider.GetValue(fieldName).AttemptedValue;
				var prop = ret.GetType().GetProperty(fieldName);
				prop.GetSetMethod().Invoke(ret, new object[] { JsonConvert.DeserializeObject(json, prop.PropertyType) });
			}
			return ret;
		}

		public static object Register(ref ModelBinderDictionary binders, params Expression<Func<T, object>>[] fieldNameExp)
		{
			var binder = new InnerJsonModelBinder<T>(fieldNameExp);
			binders.Add(typeof(T), binder);
			return binder;
		}

	}

}