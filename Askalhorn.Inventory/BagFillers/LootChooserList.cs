using System;
using System.Collections.Generic;

namespace Askalhorn.Inventory.BagFillers
{
    public class LootChooserList: ILootChooser
    {
        public List<ILootChooser> List { get; set; }
        
        public void Fill(Random random, Bag bag)
        {
            foreach (var loot in List)
                loot.Fill(random, bag);
        }
    }
}