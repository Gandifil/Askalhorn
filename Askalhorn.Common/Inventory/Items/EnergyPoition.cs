using Askalhorn.Common.Mechanics;
using Askalhorn.Common.Mechanics.Impacts;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.TextureAtlases;
using Newtonsoft.Json;

namespace Askalhorn.Common.Inventory.Items
{
    internal class EnergyPoition: Poition
    {
        public uint Value { get; set; }

        public override TextureRegion2D Texture { get; }= new (Storage.Content.Load<Texture2D>("images/items"), 
             32*3, 0, 32, 32);
        public override string Name => $"Зелье энергии +{Value}";
        public override IItem.RarityLevel Rarity { get; } = IItem.RarityLevel.Rare;
        protected internal override IImpact Impact => new AddLevelEnergyImpact((int)Value);
        // public bool Equals(IItem? other)
        // {
        //     return other is EnergyPoition item && item.Value == Value;
        // }

        public override string Description => $"Восстанавливает {Value} энергии";
        //
        // [JsonIgnore]
        // public TextureRegion2D Texture { get; } = new TextureRegion2D(Storage.Content.Load<Texture2D>("images/items"), 
        //     32*3, 0, 32, 32);
        //
        // public float Weight => 0.5f;
        //
        // IImpact IItem.Impact => new AddLevelEnergyImpact((int)Value);
        // public bool Equals(IItem? other)
        // {
        //     return other is EnergyPoition item && item.Value == Value;
        // }
    }
}