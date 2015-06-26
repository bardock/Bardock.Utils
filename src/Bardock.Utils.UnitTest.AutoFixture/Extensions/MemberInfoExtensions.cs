using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bardock.Utils.UnitTest.AutoFixture.Extensions
{
    public static class MemberInfoExtensions
    {
        public static object GetValue(this MemberInfo member, object target)
        {
            object value = null;
            switch (member.MemberType)
            {
                case MemberTypes.Field:
                    value = ((FieldInfo)member).GetValue(target);
                    break;
                case MemberTypes.Property:
                    value = ((PropertyInfo)member).GetValue(target);
                    break;
                default:
                    throw new ArgumentException("MemberInfo must be of type FieldInfo or PropertyInfo", "member");
            }
            return value;
        }
    }
}
