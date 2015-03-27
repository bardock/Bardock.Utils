using Bardock.Utils.UnitTest.Data.EF.Effort.DataLoaders;
using System.Collections.Generic;

public class SlotDataLoader : IEntityDataLoader<Slot>{

    public IEnumerable<Slot> GetData()
    {
        return new List<Slot>() 
        {
                    };
    }

}
