using System;

namespace Bardock.Utils.Data.EF.Annotations
{
    /// <summary>
    /// Specifies the database table that a DbSet is mapped to
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DbSetTableAttribute : Attribute
    {
        public DbSetTableAttribute(string name = null, string schema = null)
        {
            Name = name;
            Schema = schema;
        }

        public string Name { get; protected set; }

        public string Schema { get; protected set; }
    }
}