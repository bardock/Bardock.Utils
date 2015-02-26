﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bardock.Utils.UnitTest.Data
{
    public interface IDataContextWrapper
    {
        IQueryable<T> GetQuery<T>()
            where T : class;

        IDataContextWrapper Add<T>(T e)
            where T : class;

        IDataContextWrapper Add<T>(IEnumerable<T> e)
            where T : class;

        IDataContextWrapper Update<T>(T e)
            where T : class;

        IDataContextWrapper Update<T>(IEnumerable<T> e)
            where T : class;

        IDataContextWrapper Delete<T>(T e)
            where T : class;

        IDataContextWrapper Delete<T>(IEnumerable<T> e)
            where T : class;

        IDataContextWrapper Detach<T>(T e)
            where T : class;

        IDataContextWrapper Detach<T>(IEnumerable<T> e)
            where T : class;

        IDataContextWrapper Save();

        Task<IDataContextWrapper> SaveAsync();
    }
}