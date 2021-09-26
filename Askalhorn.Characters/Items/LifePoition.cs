using Askalhorn.Characters.Impacts;
using Askalhorn.Common;
using Askalhorn.Inventory;
using Askalhorn.Inventory.Items;
using Askalhorn.Render;
using Askalhorn.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;
using Newtonsoft.Json;

namespace Askalhorn.Characters.Items
{
    internal class LifePoition: Poition
    {
        public uint Value { get; }
        public override ItemRarity ItemRarity => ItemRarity.Rare;
        protected override IImpact Impact => new HealImpact((int)Value);
        public bool Equals(IItem? other)
        {
            return other is LifePoition item && item.Value == Value;
        }

        public LifePoition(uint value)
        {
            Value = value;
            
            Name = new MockSimpleTextPointer($"Лечебное зелье +{value}");
            Description = new MockSimpleTextPointer($"Восстанавливает {value} HP");
            Renderer = new TextureRenderer("items", new(0), new(32));
        }
    }
}