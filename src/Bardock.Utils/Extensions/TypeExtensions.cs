using System;

namespace Bardock.Utils.Extensions
{
    public static class TypeExtensions
    {
        public static Type GetNullableUnderlyingType(this Type t)
        {
            return Nullable.GetUnderlyingType(t);
        }

        public static bool IsNullable(this Type t)
        {
            return t.GetNullableUnderlyingType() != null;
        }
    }
}