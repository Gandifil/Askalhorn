using Askalhorn.Characters.Impacts;
using Askalhorn.Common;
using Askalhorn.Inventory;
using Askalhorn.Inventory.Items;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Characters.Items
{
    internal class EnergyPoition: Poition
    {
        public uint Value { get; set; }

        public override TextureRegion2D Texture { get; }= new (Storage.Content.Load<Texture2D>("images/items"), 
             32*3, 0, 32, 32);
        public override string Name => $"Зелье энергии +{Value}";
        public override IItem.RarityLevel Rarity { get; } = IItem.RarityLevel.Rare;
        protected override IImpact Impact => new AddLevelEnergyImpact((int)Value);

        public override string Description => $"Восстанавливает {Value} энергии";
    }
}