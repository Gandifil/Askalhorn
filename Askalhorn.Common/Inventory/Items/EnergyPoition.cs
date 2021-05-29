using Askalhorn.Common.Mechanics;
using Askalhorn.Common.Mechanics.Impacts;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Common.Inventory.Items
{
    public class EnergyPoition: IItem
    {
        public readonly int Value;

        public EnergyPoition(int value)
        {
            Value = value;
        }
        public string Name => $"Зелье энергии +{Value}";
        public string Description => $"Восстанавливает {Value} энергии";
        public TextureRegion2D Texture { get; } = new TextureRegion2D(Storage.Content.Load<Texture2D>("images/items"), 
            32*3, 0, 32, 32);

        public float Weight => 0.5f;
        public Size Size { get; } = new Size(1, 1);

        IImpact IItem.Impact => new AddLevelEnergyImpact(Value);
    }
}