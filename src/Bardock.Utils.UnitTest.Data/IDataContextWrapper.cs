using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bardock.Utils.UnitTest.Data
{
    public interface IDataContextWrapper
    {
        IQueryable<T> GetQuery<T>() 
            where T : class;

        IDataContextWrapper Add<T>(T e) 
            where T : class;

        IDataContextWrapper Update<T>(T e) 
            where T : class;

        IDataContextWrapper Save();

        Task<IDataContextWrapper> SaveAsync();
    }
}
