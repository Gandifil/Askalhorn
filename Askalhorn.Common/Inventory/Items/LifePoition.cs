using Askalhorn.Common.Mechanics;
using Askalhorn.Common.Mechanics.Impacts;
using Askalhorn.Common.Render;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.TextureAtlases;
using Newtonsoft.Json;

namespace Askalhorn.Common.Inventory.Items
{
    internal class LifePoition: Poition
    {
        public uint Value { get; set; }
        public override string Name => $"Лечебное зелье +{Value}";
        public override IItem.RarityLevel Rarity => IItem.RarityLevel.Rare;
        protected internal override IImpact Impact => new HealImpact((int)Value);
        public override string Description => $"Восстанавливает {Value} HP";
        
        [JsonIgnore]
        public override TextureRegion2D Texture { get; } = new TextureRegion2D(Storage.Content.Load<Texture2D>("images/items"), 
            0, 0, 32, 32);
        public bool Equals(IItem? other)
        {
            return other is LifePoition item && item.Value == Value;
        }
    }
}