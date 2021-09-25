using System;
using Askalhorn.Inventory.Items;

namespace Askalhorn.Inventory.BagFillers
{
    public class StaticLootChooser: ILootChooser
    {
        public IItem Item { get; set; }

        public int Minimum { get; set; } = 1;

        public int Maximum { get; set; } = 1;
        
        public void Fill(Random random, Bag bag)
        {
            var count = random.Next(Minimum, Maximum + 1);
            for (int i = 0; i < count; i++)
                bag.Put(Item);
        }
    }
}