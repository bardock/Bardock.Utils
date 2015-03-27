using Bardock.Utils.UnitTest.Data.EF.Effort.DataLoaders;
using System.Collections.Generic;

public class SlotEventTargetDataLoader : IEntityDataLoader<SlotEventTarget>{

    public IEnumerable<SlotEventTarget> GetData()
    {
        return new List<SlotEventTarget>() 
        {
                    };
    }

}
