using System;

namespace Bardock.Utils.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsPrimitive(this Type type)
        {
            return (type == typeof(object) || Type.GetTypeCode(type) != TypeCode.Object);
        }
    }
}