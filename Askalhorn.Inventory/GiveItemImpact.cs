using System;
using Askalhorn.Common;
using Askalhorn.Common.Interpetators;
using Askalhorn.Inventory.Items;
using MonoGame.Extended.TextureAtlases;
using Newtonsoft.Json;

namespace Askalhorn.Inventory
{
    public class GiveItemImpact: IImpact
    {
        public string Description { get; }
        public TextureRegion2D TextureRegion { get; }

        public IItem Item { get; }

        public IExpression<uint> Count { get; }
        
        [JsonConstructor]
        public GiveItemImpact(IItem item, IExpression<uint> count)
        {
            Item = item;
            Count = count;
        }

        [CommandConstructor]
        public GiveItemImpact(string item, uint count = 1)
        {
            Item = new ContentItem(item);
            Count = new StaticExpression<uint>(count);
        }
        
        public void On(object target)
        {
            var wBag = target as IHasBag;
            if (wBag is null)
                throw new ArgumentNullException(nameof(target), "Target must be a " + nameof(IHasBag));
            
            wBag.Bag.Put(Item, Count.Generate(null, new Random()));
        }
    }
}