using Askalhorn.Common.Localization;
using Askalhorn.Common.Mechanics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Common.Inventory.Items
{
    public class Dagger: IItem
    {
        public TextureRegion2D Texture { get; } = Storage.Load("effects", 1, 1);
        public string TooltipText => TextBank.Get("rarity", 0).WithColor("Blue");
        
        public bool Equals(IItem? other)
        {
            return other is Dagger;
        }

        public string Name  => "Кинжал";
        public IItem.PurposeType Type  => IItem.PurposeType.Weapon;
        public float Weight => 1;

        IImpact IItem.Impact => null;
    }
}