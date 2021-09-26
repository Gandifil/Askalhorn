using System;
using System.Collections.Generic;
using Askalhorn.Characters.Effects;
using Askalhorn.Characters.Impacts;
using Askalhorn.Characters.Interpretators;
using Askalhorn.Combat;
using Askalhorn.Common;
using Askalhorn.Common.Interpetators;
using Askalhorn.Render;
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

        public override TextureRenderer Renderer => new TextureRenderer("effects", new(0, 0));

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
                    TooltipText = "Боевая концентрация\nДополнительно защищает от 10 единиц физического урона.",
                    Renderer = new TextureRenderer("effects", new(2, 0)),
                },
                new IAbility.Modification()
                {
                    TooltipText = "Обмен энергий\nМедитация восстанавливает значительно быстрее, зависит от магической силы, стоимость увеличивается.",
                    Renderer = new TextureRenderer("effects", new(3, 0)),
                },
                new IAbility.Modification()
                {
                    TooltipText = "Истинный покой\nВосстанавливает дополнительно 3% здоровья в секунду.",
                    Renderer = new TextureRenderer("effects", new(4, 0)),
                },
            };

        public int HealPercent => 2 + (CurrentModification == 2 ? 3 : 0);

        private IExpression<float> HealAbsValue =
            new MultiExpression<float>
            {
                First = new StaticExpression<float>(0.1f),
                Second = new SecondaryExpression()
                {
                    Type = SecondaryType.MagicPower,
                }
            };

        public uint EffectTurn => CurrentModification == 1 ? (uint)5 : 10;

        public override void Use(Character character, Character target)
        {
            var healEffect = new ImpactEffect(
                CurrentModification == 1 ? 
                    new HealImpact((int)HealAbsValue.Generate(character, new Random())) :
                new HealPercentImpact{ Value = HealPercent});

            if (CurrentModification == 0)
            {
                new TempEffectImpact(new CollectionEffect()
                {
                    Effects = new List<IEffect>()
                    {
                        healEffect,
                        new ProtectEffect()
                        {
                            Type = DamageType.Piercing,
                            Value = 10,
                        },
                    }
                }, EffectTurn).On(character);
            }
            else
            { 
                new TempEffectImpact(healEffect, EffectTurn).On(character);
            }
        }
    }
}