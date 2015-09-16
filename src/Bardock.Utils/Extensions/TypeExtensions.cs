using System;
using System.Linq;

namespace Bardock.Utils.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Determines whether the specified Type is primitive or not.
        /// </summary>
        /// <param name="type">The System.Type to evaluate</param>
        public static bool IsPrimitive(this Type type)
        {
            if (type.IsNullable())
                type = type.GetNullableUnderlyingType();

            return Type.GetTypeCode(type) != TypeCode.Object
                || type.In(
                    typeof(object),
                    typeof(Enum),
                    typeof(Guid),
                    typeof(DateTime),
                    typeof(DateTimeOffset),
                    typeof(TimeSpan));
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

        /// <summary>
        /// Determines whether the <paramref name="givenType"/> is assignable to
        /// <paramref name="genericType"/> taking into account generic definitions (e.g., IFoo<int> is assignable to IFoo<>).
        /// Credits: http://tmont.com/blargh/2011/3/determining-if-an-open-generic-type-isassignablefrom-a-type
        /// </summary>
        public static bool IsAssignableToGenericType(this Type givenType, Type genericType)
        {
            if (givenType == null || genericType == null)
                return false;

            return givenType == genericType
              || givenType.MapsToGenericTypeDefinition(genericType)
              || givenType.HasInterfaceThatMapsToGenericTypeDefinition(genericType)
              || givenType.BaseType.IsAssignableToGenericType(genericType);
        }

        private static bool HasInterfaceThatMapsToGenericTypeDefinition(this Type givenType, Type genericType)
        {
            return givenType
              .GetInterfaces()
              .Where(it => it.IsGenericType)
              .Any(it => it.GetGenericTypeDefinition() == genericType);
        }

        private static bool MapsToGenericTypeDefinition(this Type givenType, Type genericType)
        {
            return genericType.IsGenericTypeDefinition
              && givenType.IsGenericType
              && givenType.GetGenericTypeDefinition() == genericType;
        }
    }
}