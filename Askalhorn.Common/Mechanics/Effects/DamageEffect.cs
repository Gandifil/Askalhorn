using Askalhorn.Common.Mechanics.Impacts;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Common.Mechanics.Effects
{
    internal class DamageEffect: Effect
    {
        public readonly DamageImpact impact;
        
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