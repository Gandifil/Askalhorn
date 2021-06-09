using Askalhorn.Common.Mechanics;
using Askalhorn.Common.Mechanics.Impacts;
using Askalhorn.Common.Render;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.TextureAtlases;
using Newtonsoft.Json;

namespace Askalhorn.Common.Inventory.Items
{
    class LifePoition: IItem
    {
        public uint Value { get; set; }
        public string Name => $"Лечебное зелье +{Value}";
        public string Description => $"Восстанавливает {Value} HP";
        
        [JsonIgnore]
        public TextureRegion2D Texture { get; } = new TextureRegion2D(Storage.Content.Load<Texture2D>("images/items"), 
            0, 0, 32, 32);

        public float Weight => 0.5f;

        IImpact IItem.Impact => new HealImpact((int)Value);
        public bool Equals(IItem? other)
        {
            return other is LifePoition item && item.Value == Value;
        }
    }
}