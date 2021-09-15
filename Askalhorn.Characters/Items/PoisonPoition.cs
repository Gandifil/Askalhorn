using Askalhorn.Characters.Effects;
using Askalhorn.Characters.Impacts;
using Askalhorn.Common;
using Askalhorn.Inventory;
using Askalhorn.Inventory.Items;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;
using Newtonsoft.Json;

namespace Askalhorn.Characters.Items
{
    public class PoisonPoition: Poition
    {
        public uint Value { get; set; }
        public uint TurnCount { get; set; }
        
        public override string Name => $"Ядовитое зелье -{Value}";
        public override IItem.RarityLevel Rarity => IItem.RarityLevel.Rare;
        protected override IImpact Impact => new EffectImpact(new ImpactEffect(new DamageImpact((int)Value), TurnCount));
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