using Bardock.Utils.UnitTest.Data.EF.Effort.DataLoaders;
using System.Collections.Generic;

public class LanguageDataLoader : IEntityDataLoader<Language>{

    public IEnumerable<Language> GetData()
    {
        return new List<Language>() 
        {
                    };
    }

}
