using Askalhorn.Common;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Inventory.Items
{
    public class Dagger: Item
    {
        public override TextureRegion2D Texture { get; } = Storage.Load("effects", 1, 1);
        public override string Name { get; } = "Простой кинжал";
        public override IItem.PurposeType Type { get; } = IItem.PurposeType.Weapon;
        public override IItem.RarityLevel Rarity { get; } = IItem.RarityLevel.Rare;
        protected override IImpact Impact { get; } = null;
        public override string Description { get; } = "Обычный железный кинжал";
    }
}