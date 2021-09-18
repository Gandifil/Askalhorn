﻿using Askalhorn.Common;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Characters.Effects
{
    public class ImpactEffect: Effect
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

        public override string TooltipText => Impact.Description + " каждый ход";
        public override TextureRegion2D Texture => Impact.TextureRegion;
    }
}