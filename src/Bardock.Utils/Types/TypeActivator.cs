using System;
using System.Reflection;

namespace Bardock.Utils.Types
{
    public static class TypeActivator
    {
        public static object CreateFromFullName(string fullName)
        {
            string[] assemblyAndTypeNames = fullName.Split(',');
            var assembly = Assembly.Load(assemblyAndTypeNames[1].Trim());
            var type = assembly.GetType(assemblyAndTypeNames[0].Trim());
            return Activator.CreateInstance(type);
        }
    }
}