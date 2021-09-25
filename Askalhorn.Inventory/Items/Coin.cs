using Askalhorn.Common;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Inventory.Items
{
    public class Coin: Item
    {
        public override ItemPurpose Type => ItemPurpose.Currency;
        public override ItemRarity ItemRarity => ItemRarity.Casual;
        public override float Weight => .008f;
        protected override IImpact Impact => null;
    }
}