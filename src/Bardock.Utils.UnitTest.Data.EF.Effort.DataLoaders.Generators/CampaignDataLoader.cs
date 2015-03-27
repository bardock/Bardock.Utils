using Bardock.Utils.UnitTest.Data.EF.Effort.DataLoaders;
using System.Collections.Generic;

public class CampaignDataLoader : IEntityDataLoader<Campaign>{

    public IEnumerable<Campaign> GetData()
    {
        return new List<Campaign>() 
        {
                    };
    }

}
