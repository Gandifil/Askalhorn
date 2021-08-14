using Askalhorn.Common.Mechanics;
using Askalhorn.Common.Mechanics.Effects;
using Askalhorn.Common.Mechanics.Impacts;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.TextureAtlases;
using Newtonsoft.Json;

namespace Askalhorn.Common.Inventory.Items
{
    internal class PoisonPoition: Poition
    {
        public uint Value { get; set; }
        public uint TurnCount { get; set; }
        
        public override string Name => $"Ядовитое зелье -{Value}";
        public override IItem.RarityLevel Rarity => IItem.RarityLevel.Rare;
        protected internal override IImpact Impact => new EffectImpact(new ImpactEffect(new DamageImpact((int)Value), TurnCount));
        public override string Description => $"Забирает {Value} HP в течении {TurnCount} ходов";
        [JsonIgnore]
        public override TextureRegion2D Texture { get; } = new TextureRegion2D(Storage.Content.Load<Texture2D>("images/items"), 
            2*32, 0, 32, 32);

        public bool Equals(IItem? other)
        {
            return other is PoisonPoition item && item.Value == Value && item.TurnCount == TurnCount;
        }
    }
}