using System;

namespace Bardock.Utils.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Determines whether the specified Type is primitive or not.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsPrimitive(this Type type)
        {
            return (type == typeof(object) || Type.GetTypeCode(type) != TypeCode.Object);
        }

        /// <summary>
        /// Returns the underlying type argument of the specified nullable type.
        /// </summary>
        /// <returns>The type argument of the nullableType parameter, if the nullableType
        /// parameter is a closed generic nullable type; otherwise, null.</returns>
        public static Type GetNullableUnderlyingType(this Type t)
        {
            return Nullable.GetUnderlyingType(t);
        }

        /// <summary>
        /// Gets a value indicating whether the System.Type is a nullable or not nullable type.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool IsNullable(this Type t)
        {
            return t.GetNullableUnderlyingType() != null;
        }

        /// <summary>
        /// Gets a value indicating whether the System.Type represents a nullable or not nullable
        /// type parameter in the definition of a generic type or method.
        /// </summary>
        public static bool IsGenericParameter(this Type t, bool nullable)
        {
            return nullable && t.IsNullable() && t.GetNullableUnderlyingType().IsGenericParameter
                || !nullable && !t.IsNullable() && t.IsGenericParameter;
        }

        /// <summary>
        /// Gets a value indicating whether the System.Type is a nullable or not nullable value type.
        /// </summary>
        public static bool IsValueType(this Type t, bool nullable)
        {
            return nullable && t.IsNullable() && t.GetNullableUnderlyingType().IsValueType
                || !nullable && !t.IsNullable() && t.IsValueType;
        }
    }
}