using Askalhorn.Characters.Impacts;
using Askalhorn.Common;
using Askalhorn.Inventory;
using Askalhorn.Inventory.Items;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;
using Newtonsoft.Json;

namespace Askalhorn.Characters.Items
{
    internal class LifePoition: Poition
    {
        public uint Value { get; set; }
        public override string Name => $"Лечебное зелье +{Value}";
        public override IItem.RarityLevel Rarity => IItem.RarityLevel.Rare;
        protected override IImpact Impact => new HealImpact((int)Value);
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