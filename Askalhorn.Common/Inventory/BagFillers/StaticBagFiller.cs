using System;

namespace Askalhorn.Common.Inventory.BagFillers
{
    public class StaticBagFiller: IBagFiller
    {
        public IItem Item { get; set; }

        public int Minimum { get; set; } = 1;

        public int Maximum { get; set; } = 1;
        
        public void Fill(Random random, IBag bag)
        {
            var count = random.Next(Minimum, Maximum + 1);
            for (int i = 0; i < count; i++)
                bag.Put(Item);
        }
    }
}