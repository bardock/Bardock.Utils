using Bardock.Utils.UnitTest.Data.EF.Effort.DataLoaders;
using System.Collections.Generic;

public class ContentDataLoader : IEntityDataLoader<Content>{

    public IEnumerable<Content> GetData()
    {
        return new List<Content>() 
        {
                    };
    }

}
