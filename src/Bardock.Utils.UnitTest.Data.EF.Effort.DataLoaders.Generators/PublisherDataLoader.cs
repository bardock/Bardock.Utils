using Bardock.Utils.UnitTest.Data.EF.Effort.DataLoaders;
using System.Collections.Generic;

public class PublisherDataLoader : IEntityDataLoader<Publisher>{

    public IEnumerable<Publisher> GetData()
    {
        return new List<Publisher>() 
        {
                    };
    }

}
