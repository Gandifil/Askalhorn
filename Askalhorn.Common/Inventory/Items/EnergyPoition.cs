using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Common.Inventory.Items
{
    public class EnergyPoition: IItem
    {
        public string Name => "Зелье энергии";
        public string Description => "Восстанавливает 50 энергии";
        public TextureRegion2D Texture { get; } = new TextureRegion2D(Storage.Content.Load<Texture2D>("images/items"), 
            32, 0, 32, 32);

        public float Weight => 0.5f;
        public Size Size { get; } = new Size(1, 1);
    }
}