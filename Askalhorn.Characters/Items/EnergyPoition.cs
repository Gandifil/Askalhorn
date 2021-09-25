using Askalhorn.Characters.Impacts;
using Askalhorn.Common;
using Askalhorn.Inventory;
using Askalhorn.Inventory.Items;
using Askalhorn.Render;
using Askalhorn.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Characters.Items
{
    internal class EnergyPoition: Poition
    {
        public uint Value { get; }
        
        public override ItemRarity ItemRarity { get; } = ItemRarity.Rare;
        protected override IImpact Impact => new AddLevelEnergyImpact((int)Value);

        public EnergyPoition(uint value)
        {
            Value = value;
            
            Name = new MockTextPointer($"Зелье энергии +{value}");
            Description = new MockTextPointer($"Восстанавливает {value} энергии");
            Renderer = new TextureRenderer("items", new(3, 0), new Point(32));
        }
    }
}