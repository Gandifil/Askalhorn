using Askalhorn.Common.Localization;
using Askalhorn.Common.Mechanics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Common.Inventory.Items
{
    internal class Dagger: Item
    {
        // public TextureRegion2D Texture { get; } = Storage.Load("effects", 1, 1);
        // public string TooltipText => TextBank.Get("rarity", 0).WithColor("Blue");
        //
        // public bool Equals(IItem? other)
        // {
        //     return other is Dagger;
        // }
        //
        // public string Name  => "Кинжал";
        // public IItem.PurposeType Type  => IItem.PurposeType.Weapon;
        // public float Weight => 1;
        public override TextureRegion2D Texture { get; } = Storage.Load("effects", 1, 1);
        public override string Name { get; } = "Простой кинжал";
        public override IItem.PurposeType Type { get; } = IItem.PurposeType.Weapon;
        public override IItem.RarityLevel Rarity { get; } = IItem.RarityLevel.Rare;
        protected internal override IImpact Impact { get; } = null;
        public override string Description { get; } = "Обычный железный кинжал";
    }
}