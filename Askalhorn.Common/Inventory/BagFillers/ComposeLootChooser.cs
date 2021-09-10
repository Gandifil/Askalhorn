using System;
using System.Collections;
using System.Collections.Generic;

namespace Askalhorn.Common.Inventory.BagFillers
{
    public class ComposeLootChooser: ILootChooser
    {
        public IEnumerable<ILootChooser> LootChoosers { get; set; }
        
        public void Fill(Random random, IBag bag)
        {
            foreach (var loot in LootChoosers)
                loot.Fill(random, bag);
        }
    }
}