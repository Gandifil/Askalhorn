using System;
using System.Collections.Generic;

namespace Askalhorn.Inventory.BagFillers
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