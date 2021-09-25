using System;
using Askalhorn.Common.Interpetators;
using Askalhorn.Inventory.Items;
using Newtonsoft.Json;

namespace Askalhorn.Inventory.Expressions
{
    public class HasItemExpression: BoolExpression
    {
        public IItem Item { get; }

        public uint Count { get; }

        [JsonConstructor]
        public HasItemExpression(string item, uint count)
        {
            Item = new ContentItem(item);
            Count = count;
        }
        
        protected override bool PrivateCalculate(object target, Random random)
        {
            var hasBag = target as IHasBag;
            if (hasBag is null)
                throw new ArgumentNullException("Target must be " + nameof(IHasBag));

            return hasBag.Bag.Contains(Item, Count);
        }
    }
}