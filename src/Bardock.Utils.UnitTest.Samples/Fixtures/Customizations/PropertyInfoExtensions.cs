using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bardock.Utils.UnitTest.Samples.Fixtures.Customizations
{
    public static class PropertyInfoExtensions
    {
        public static TAttribute GetCustomAttribute<TAttribute>(this PropertyInfo pi, bool inherit)
            where TAttribute : Attribute
        {
            return (TAttribute)pi.GetCustomAttributes(typeof(TAttribute), inherit).FirstOrDefault();
        }
    }
}
