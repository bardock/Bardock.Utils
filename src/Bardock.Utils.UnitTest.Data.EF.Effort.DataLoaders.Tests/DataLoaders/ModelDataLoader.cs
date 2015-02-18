using Bardock.Utils.UnitTest.Data.EF.Effort.DataLoaders.Tests.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bardock.Utils.UnitTest.Data.EF.Effort.DataLoaders.Tests.DataLoaders
{
    public class ModelDataLoader : IEntityDataLoader<Model>
    {
        public IEnumerable<Model> GetData()
        {
            yield return new Model()
            {
                Id = 1,
                Name = "Test",
                Description = "TST"
            };
        }
    }
}
