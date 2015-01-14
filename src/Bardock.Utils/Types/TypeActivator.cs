using System;

namespace Bardock.Utils.Types
{
    public static class TypeActivator
    {
        /// <summary>
        /// Gets an instance of the Type with the specified name, performing a case-sensitive search.
        /// </summary>
        /// <param name="typeName">
        /// The assembly-qualified name of the type to get.
        /// If the type is in the currently executing assembly or in Mscorlib.dll, it is sufficient to supply the type name qualified by its namespace.
        /// </param>
        public static object CreateFromTypeName(string typeName)
        {
            return Activator.CreateInstance(Type.GetType(typeName));
        }
    }
}