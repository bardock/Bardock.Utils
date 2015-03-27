using Bardock.Utils.UnitTest.Data.EF.Effort.DataLoaders;
using System.Collections.Generic;

public class SlotEventDataLoader : IEntityDataLoader<SlotEvent>{

    public IEnumerable<SlotEvent> GetData()
    {
        return new List<SlotEvent>() 
        {
                    };
    }

}
