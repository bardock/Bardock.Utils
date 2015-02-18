using Bardock.Utils.Linq.Expressions;
using Effort.DataLoaders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Reflection;

namespace Bardock.Utils.UnitTest.Data.EF.Effort.DataLoaders.Tests.Helpers
{
    public class TableDescriptionBuilder<T>
        where T : class
    {
        private string _name;
        private IList<ColumnDescription> _columns;

        public TableDescriptionBuilder()
            : this(typeof(T).Name) { }

        public TableDescriptionBuilder(string name)
        {
            _name = name;
            _columns = new List<ColumnDescription>();
        }

        public void AddColumn(Expression<Func<T, object>> columnSelector)
        {
            var typedExpression = ExpressionHelper.RemoveConvert(columnSelector);
            var memberExpression = (MemberExpression)typedExpression.Body;
            var propertyInfo = (PropertyInfo)memberExpression.Member;

            _columns.Add(
                CreateColumnDescription(
                    propertyInfo.Name,
                    propertyInfo.PropertyType));
        }

        public TableDescription Build()
        {
            return CreateTableDescription(_name, new ReadOnlyCollection<ColumnDescription>(_columns));
        }

        private TableDescription CreateTableDescription(string name, IReadOnlyCollection<ColumnDescription> columns)
        {
            return ActivatorHelper.ActivateNonPublic<TableDescription>(name, columns);
        }

        private ColumnDescription CreateColumnDescription(string name, Type type)
        {
            return ActivatorHelper.ActivateNonPublic<ColumnDescription>(name, type);
        }
    }
}