using Askalhorn.Common.Mechanics.Effects;
using Askalhorn.Common.Mechanics.Impacts;

namespace Askalhorn.Common.Mechanics.Abilities
{
    internal class HealMeditation: Ability
    {
        public HealMeditation()
        {
            Name = "Восстанавливающая медитация";
            Description = "Восстанавливающая медитация";
            Icon = Storage.Load("effects", 0, 0);
            CoolDown = 10;
        }

        protected override void Use(Character character, Character target)
        {
            new EffectImpact(new ImpactEffect(new HealImpact(1), 5)).On(character);
        }
    }
}