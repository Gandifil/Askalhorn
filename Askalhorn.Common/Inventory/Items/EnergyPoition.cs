using Askalhorn.Common.Mechanics;
using Askalhorn.Common.Mechanics.Impacts;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.TextureAtlases;
using Newtonsoft.Json;

namespace Askalhorn.Common.Inventory.Items
{
    public class EnergyPoition: IItem
    {
        public uint Value { get; set; }
        
        public string Name => $"Зелье энергии +{Value}";
        public string Description => $"Восстанавливает {Value} энергии";
        
        [JsonIgnore]
        public TextureRegion2D Texture { get; } = new TextureRegion2D(Storage.Content.Load<Texture2D>("images/items"), 
            32*3, 0, 32, 32);

        public float Weight => 0.5f;

        IImpact IItem.Impact => new AddLevelEnergyImpact((int)Value);
    }
}