using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Common.Mechanics.Effects
{
    internal class ImpactEffect: Effect
    {
        public readonly IImpact Impact;
        
        public ImpactEffect(IImpact impact, uint time) : base(time)
        {
            Impact = impact;
        }

        public override void Tick(Character character)
        {
            Impact.On(character);
        }

        public override string Description => Impact.Description + " каждый ход";
        public override TextureRegion2D TextureRegion => Impact.TextureRegion;
    }
}