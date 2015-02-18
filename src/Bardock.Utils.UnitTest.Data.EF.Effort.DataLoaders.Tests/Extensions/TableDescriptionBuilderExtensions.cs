using Bardock.Utils.UnitTest.Data.EF.Effort.DataLoaders.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
