using Bardock.Utils.UnitTest.Data.EF.Effort.DataLoaders.Tests.Helpers;
using System;
using System.Linq.Expressions;

namespace Bardock.Utils.UnitTest.Data.EF.Effort.DataLoaders.Tests.Extensions
{
    public static class TableDescriptionBuilderExtensions
    {
        public static TableDescriptionBuilder<T> Add<T>(this TableDescriptionBuilder<T> @this, Expression<Func<T, object>> columnSelector)
            where T : class
        {
            @this.AddColumn(columnSelector);
            return @this;
        }
    }
}