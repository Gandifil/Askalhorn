using System;
using System.Collections;
using System.Collections.Generic;

namespace Askalhorn.Common.Inventory.BagFillers
{
    public class ComposeBagFiller: IBagFiller
    {
        public IEnumerable<IBagFiller> Fillers { get; set; }
        
        public void Fill(Random random, IBag bag)
        {
            foreach (var filler in Fillers)
                filler.Fill(random, bag);
        }
    }
}