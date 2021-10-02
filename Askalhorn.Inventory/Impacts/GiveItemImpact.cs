using System;
using Askalhorn.Common;
using Askalhorn.Common.Interpetators;
using Askalhorn.Inventory.Items;
using Askalhorn.Text;
using MonoGame.Extended.TextureAtlases;
using Newtonsoft.Json;
using Serilog;

namespace Askalhorn.Inventory.Impacts
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
            Count = count ?? new StaticExpression<uint>(1);
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

            var count = Count.Generate(null, new Random());
            wBag.Bag.Put(Item, count);
            
            Log.Information(new TextPointer("inventory", "Add_Item").ToString(), Item.Name, count);
        }
    }
}