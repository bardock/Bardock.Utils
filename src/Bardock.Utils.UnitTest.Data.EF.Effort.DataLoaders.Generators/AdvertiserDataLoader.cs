using Bardock.Utils.UnitTest.Data.EF.Effort.DataLoaders;
using System.Collections.Generic;

public class AdvertiserDataLoader : IEntityDataLoader<Advertiser>{

    public IEnumerable<Advertiser> GetData()
    {
        return new List<Advertiser>() 
        {
                    };
    }

}
