using System;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Common.Mechanics.Impacts
{
    internal class EffectImpact: IImpact
    {
        public readonly Effect effect;

        public EffectImpact(Effect effect)
        {
            this.effect = effect;
        }
        public string Description => "";
        public TextureRegion2D TextureRegion => throw new NotImplementedException();
        
        public void On(Character character)
        {
            character.Effects.Add(effect);
        }
    }
}