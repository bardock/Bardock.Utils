using Bardock.Utils.UnitTest.Data.EF.Effort.DataLoaders;
using System.Collections.Generic;

public class CountryDataLoader : IEntityDataLoader<Country>{

    public IEnumerable<Country> GetData()
    {
        return new List<Country>() 
        {
                    };
    }

}
