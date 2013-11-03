using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Bardock.Utils.Web.Mvc.Helpers
{
	public class SelectListHelper
	{
		public static System.Web.Mvc.SelectList Create<T, TValue, TText>(
            IEnumerable<T> items, 
            Expression<Func<T, TValue>> dataValueExpression, 
            Expression<Func<T, TText>> dataTextExpression, 
            object selectedValue = null)
		{
			return new System.Web.Mvc.SelectList(
                items: items, 
                dataValueField: ((MemberExpression)dataValueExpression.Body).Member.Name, 
                dataTextField: ((MemberExpression)dataTextExpression.Body).Member.Name, 
                selectedValue: selectedValue
            );
		}
	}
}