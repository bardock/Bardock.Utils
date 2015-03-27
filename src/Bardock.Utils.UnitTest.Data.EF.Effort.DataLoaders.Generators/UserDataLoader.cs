using Bardock.Utils.UnitTest.Data.EF.Effort.DataLoaders;
using System.Collections.Generic;

public class UserDataLoader : IEntityDataLoader<User>{

    public IEnumerable<User> GetData()
    {
        return new List<User>() 
        {
                    };
    }

}
