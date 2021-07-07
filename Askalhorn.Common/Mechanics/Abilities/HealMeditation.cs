using Askalhorn.Common.Mechanics.Effects;
using Askalhorn.Common.Mechanics.Impacts;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Common.Mechanics.Abilities
{
    internal class HealMeditation: Ability
    {
        public override string Name => "Восстанавливающая медитация";
        
        public override string Description => "Восстанавливающая медитация";
        
        public override TextureRegion2D Icon => Storage.Load("effects", 0, 0);
        
        public override int CoolDown => 10;
        public override int MagicCost => 100;

        protected override void Use(Character character, Character target)
        {
            new EffectImpact(new ImpactEffect(new HealImpact(1), 5)).On(character);
        }
    }
}