using System;
using System.Collections.Generic;
using Askalhorn.Characters.Effects;
using Askalhorn.Characters.Impacts;
using Askalhorn.Characters.Interpretators;
using Askalhorn.Combat;
using Askalhorn.Common;
using Askalhorn.Common.Interpetators;
using Microsoft.Xna.Framework.Audio;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Characters.Abilities
{
    public class HealMeditation: Ability
    {
        public override string Name => "Восстанавливающая медитация";
        
        public override string Description => 
            $"Концентрация на собственном теле восстанавливает {_desc} здоровья в течении {EffectTurn} ходов";

        private string _desc => CurrentModification == 1 ? $"<c LightBlue>{HealAbsValue}</c> единиц" : $"{HealPercent} %";

        public override TextureRegion2D Texture => Storage.Load("effects", 0, 0);

        public override IAbility.TargetType Type => IAbility.TargetType.Self;
        public override int Radius => 10;
        
        public override int CoolDown => 10;
        public override int MagicCost => CurrentModification == 1 ? 200 : 100;
        public override uint MaxSkill => 10;
        public override SoundEffect CastSound => Storage.Content.Load<SoundEffect>("sounds\\heal");

        public override List<IAbility.Modification> Modifications { get; } =
            new()
            {
                new IAbility.Modification()
                {
                    Description = "Боевая концентрация\nДополнительно защищает от 10 единиц физического урона.",
                    Icon = Storage.Load("effects", 2, 0),
                },
                new IAbility.Modification()
                {
                    Description = "Обмен энергий\nМедитация восстанавливает значительно быстрее, зависит от магической силы, стоимость увеличивается.",
                    Icon = Storage.Load("effects", 3, 0),
                },
                new IAbility.Modification()
                {
                    Description = "Истинный покой\nВосстанавливает дополнительно 3% здоровья в секунду.",
                    Icon = Storage.Load("effects", 4, 0),
                },
            };

        public int HealPercent => 2 + (CurrentModification == 2 ? 3 : 0);

        private IExpression<float> HealAbsValue =
            new MultiExpression<float>
            {
                First = new StaticExpression<float>(0.1f),
                Second = new SecondaryExpression()
                {
                    Type = SecondaryTypes.MagicPower,
                }
            };

        public uint EffectTurn => CurrentModification == 1 ? (uint)5 : 10;

        public override void Use(Character character, Character target)
        {
            var args = new CharacterExpressionArgs()
            {
                Random = new Random(),
                Character = character,
            };
            var healEffect = new ImpactEffect(
                CurrentModification == 1 ? 
                    new HealImpact((int)HealAbsValue.Generate(args)) :
                new HealPercentImpact{ Value = HealPercent}, EffectTurn);

            if (CurrentModification == 0)
            {
                new EffectImpact(new CollectionEffect(EffectTurn)
                {
                    Effects = new List<Effect>()
                    {
                        healEffect,
                        new ProtectEffect(EffectTurn)
                        {
                            Type = DamageTypes.Phisical,
                            Value = 10,
                        },
                    }
                }).On(character);
            }
            else
            { 
                new EffectImpact(new CollectionEffect(EffectTurn)
                {
                    Effects = new List<Effect>{healEffect}
                }).On(character);
            }
        }
    }
}