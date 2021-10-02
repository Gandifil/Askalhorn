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
    public class RemoveItemImpact: IImpact, IExpression<bool>
    {
        public string Description { get; }
        public TextureRegion2D TextureRegion { get; }

        public IItem Item { get; }

        public IExpression<uint> Count { get; }
        
        [JsonConstructor]
        public RemoveItemImpact(IItem item, IExpression<uint> count)
        {
            Item = item;
            Count = count ?? new StaticExpression<uint>(1);
        }

        [CommandConstructor]
        public RemoveItemImpact(string item, uint count = 1)
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
            wBag.Bag.Pull(Item, count);
            
            Log.Information(new TextPointer("inventory", "Remove_Item").ToString(), Item.Name, count);
        }

        public bool Generate(object target, Random random)
        {
            var wBag = target as IHasBag;
            if (wBag is null)
                throw new ArgumentNullException(nameof(target), "Target must be a " + nameof(IHasBag));

            return wBag.Bag.Contains(Item, Count.Generate(null, new Random()));
        }
    }
}