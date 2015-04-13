using System;

namespace Bardock.Utils.Globalization
{
    public static class Resources
    {
        private static IResourceProvider _current = new NullResourceProvider();

        public static IResourceProvider Current { get { return _current; } }

        public static void Register(IResourceProvider provider)
        {
            _current = provider;
        }
    }

    /// <summary>
    /// Searchs for localized strings
    /// </summary>
    public interface IResourceProvider
    {
        /// <summary>
        /// Obtains localized string by its name
        /// </summary>
        string GetValue(string name);
    }

    /// <summary>
    /// Returns name as localized string
    /// </summary>
    public class NullResourceProvider : IResourceProvider
    {
        public string GetValue(string name)
        {
            return name;
        }
    }

    /// <summary>
    /// Provides a wrapper for a strongly-typed resource class (i.e. the auto-generated class from a *.resx file)
    /// </summary>
    public class TypedClassResourceProvider : IResourceProvider
    {
        private Type _resourceType;

        public TypedClassResourceProvider(Type _resourceType)
        {
            this._resourceType = _resourceType;
        }

        /// <summary>
        /// Obtains localized string by its name
        /// </summary>
        public string GetValue(string resourceName)
        {
            var prop = _resourceType.GetProperty(resourceName);
            if (prop == null)
                return resourceName;

            return prop.GetValue(null).ToString();
        }
    }
}