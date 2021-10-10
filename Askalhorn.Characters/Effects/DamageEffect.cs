using System;
using Askalhorn.Characters.Impacts;
using Askalhorn.Common;
using Askalhorn.Render;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Characters.Effects
{
    internal class DamageEffect: IEffect
    {
        public readonly IImpact impact;
        
        public DamageEffect(uint value)
        {
            impact = new DamageImpact((int) value);
        }

        public void Turn(Character character)
        {
            impact.On(character);
        }

        public string Description { get; }

        public string TooltipText => "''";
        public event Action OnChanged;
        public TextureRenderer Renderer => null;
    }
}