using System.Collections.Generic;
using Askalhorn.Common.Mechanics.Effects;
using Askalhorn.Common.Mechanics.Impacts;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Common.Mechanics.Abilities
{
    internal class HealMeditation: Ability
    {
        public override string Name => "Восстанавливающая медитация";
        
        public override string Description => 
            $"Концентрация на собственном теле восстанавливает {HealPercent}% здоровья в течении {EffectTurn} ходов";

        public override TextureRegion2D Icon => Storage.Load("effects", 0, 0);

        public override IAbility.TargetType Type => IAbility.TargetType.Self;
        public override int Radius => 10;
        
        public override int CoolDown => 10;
        public override int MagicCost => 100;
        public override uint MaxSkill => 10;

        public override List<IAbility.Modification> Modifications { get; } =
            new()
            {
                new IAbility.Modification()
                {
                    Description = "Концентрация\n",
                    Icon = Storage.Load("effects", 2, 0),
                },
                new IAbility.Modification()
                {
                    Description = "",
                    Icon = Storage.Load("effects", 3, 0),
                },
                new IAbility.Modification()
                {
                    Description = "Истинный покой\nВосстанавливает дополнительно 3% здоровья в секунду.",
                    Icon = Storage.Load("effects", 4, 0),
                },
            };

        public int HealPercent => 2 + (CurrentModification == 2 ? 3 : 0);

        public uint EffectTurn => 10;

        protected override void Use(Character character, Character target)
        {
            new EffectImpact(new ImpactEffect(
                new HealPercentImpact
                { 
                    Value = HealPercent,
                }, EffectTurn)
            ).On(character);
        }
    }
}