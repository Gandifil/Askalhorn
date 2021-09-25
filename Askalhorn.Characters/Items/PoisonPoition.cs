using Askalhorn.Characters.Effects;
using Askalhorn.Characters.Impacts;
using Askalhorn.Common;
using Askalhorn.Inventory;
using Askalhorn.Inventory.Items;
using Askalhorn.Render;
using Askalhorn.Text;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;
using Newtonsoft.Json;

namespace Askalhorn.Characters.Items
{
    public class PoisonPoition: Poition
    {
        public uint Value { get; }
        public uint TurnCount { get; }
        public override ItemRarity ItemRarity => ItemRarity.Rare;
        protected override IImpact Impact => new EffectImpact(new ImpactEffect(new DamageImpact((int)Value), TurnCount));

        public bool Equals(IItem? other)
        {
            return other is PoisonPoition item && item.Value == Value && item.TurnCount == TurnCount;
        }

        public PoisonPoition(uint value, uint turnCount)
        {
            Value = value;
            TurnCount = turnCount;
            
            Name = new MockTextPointer($"Ядовитое зелье -{value}");
            Description = new MockTextPointer($"Забирает {value} HP в течении {turnCount} ходов");
            Renderer = new TextureRenderer("items", new(2, 0), new(32));
        }
    }
}