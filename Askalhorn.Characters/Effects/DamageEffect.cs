using Askalhorn.Characters.Impacts;
using Askalhorn.Common;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Characters.Effects
{
    internal class DamageEffect: Effect
    {
        public readonly IImpact impact;
        
        public DamageEffect(uint value, uint time) : base(time)
        {
            impact = new DamageImpact((int) value);
        }

        public override void Tick(Character character)
        {
            impact.On(character);
        }

        public override string Description => "''";
        public override TextureRegion2D TextureRegion => null;
    }
}