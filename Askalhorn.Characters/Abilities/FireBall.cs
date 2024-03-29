﻿using System;
using System.Collections.Generic;
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
    public class FireBall: Ability
    {
        public override string Name => "Огненная стрела";
        
        public override string Description => $"Заклинатель придает огню форму стрелы и направляет ее в сторону противника, нанося {_damage} урона огнем.";
        public override TextureRenderer Renderer => new TextureRenderer("effects", new(0, 1));
        public override IAbility.TargetType Type => IAbility.TargetType.Character;
        public override int Radius => 10;
        public override int CoolDown { get; } = 0;

        private int _baseMagicCost = 20;
        public override int MagicCost => _baseMagicCost / (CurrentModification == 2 ? 2 : 1);
        public override uint MaxSkill => 10;
        public override SoundEffect CastSound => Storage.LoadSound("steam");

        public override List<IAbility.Modification> Modifications { get; } =
            new()
            {
                new IAbility.Modification()
                {
                    TooltipText = "Истинное разрушение\nСтрела наносит значительно больше урона.",
                    Renderer = new TextureRenderer("effects", new(2, 1)),
                },
                new IAbility.Modification()
                {
                    TooltipText = "Поджог\nСтрела дополнительно поджигает противника, нанося 10 единиц урона в течении 2 секунд",
                    Renderer = new TextureRenderer("effects", new(3, 1)),
                },
                new IAbility.Modification()
                {
                    TooltipText = "Родство с огнем\nПридавать огню форму стрелы становится значительно легче, благодаря чему затраты магии снижаются в 2 раза.",
                    Renderer = new TextureRenderer("effects", new(4, 1)),
                },
            };

        private readonly IExpression<float> _damage;

        public FireBall()
        {
            _damage = new MultiExpression<float>
              {
                  First = new RandomRangeInterpretator<float>
                  {
                      First = new SkillRelativeExpression
                      {
                          Ability = this,
                          Min = 1.3f,
                          Max = 1.9f,
                      },
                      Second = new StaticExpression<float>(2.9f)
                  },
                  Second = new SecondaryExpression
                  {
                      Type = SecondaryType.MagicPower,
                  }
              };
        }
        
        public override void Use(Character character, Character target)
        {
            new DamageImpact((int)_damage.Generate(character, new Random())).On(target);
        }
    }
}