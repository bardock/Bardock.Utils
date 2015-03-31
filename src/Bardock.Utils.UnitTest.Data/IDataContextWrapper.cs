using System.Collections.Generic;
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

        IDataContextWrapper AddAll<T>(IEnumerable<T> e)
            where T : class;

        IDataContextWrapper Update<T>(T e)
            where T : class;

        IDataContextWrapper UpdateAll<T>(IEnumerable<T> e)
            where T : class;

        IDataContextWrapper Delete<T>(T e)
            where T : class;

        IDataContextWrapper DeleteAll<T>(IEnumerable<T> e)
            where T : class;

        /// <summary>
        /// This method applies all the changes to the database and detachs all related entities.
        /// </summary>
        /// <returns>Self</returns>
        IDataContextWrapper Save();

        Task<IDataContextWrapper> SaveAsync();
    }
}