﻿using System;
using Askalhorn.Common.Interpetators;
using Askalhorn.Inventory.Items;
using Newtonsoft.Json;

namespace Askalhorn.Inventory.BagFillers
{
    public class LootChooser: ILootChooser
    {
        public IItem Item { get; }
        
        public IExpression<uint> Count { get; }

        [JsonConstructor]
        public LootChooser(IItem item, IExpression<uint> count = null)
        {
            Item = item;
            Count = count ?? new StaticExpression<uint>(1);
        }
        
        public void Fill(Random random, Bag bag)
        {
            var count = Count.Generate(null, random);
            
            bag.Put(Item, count);
        }
    }
}