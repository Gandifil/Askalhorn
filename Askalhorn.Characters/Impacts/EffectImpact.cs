using System;
using Askalhorn.Common;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Characters.Impacts
{
    public class EffectImpact: IImpact
    {
        public readonly Effect effect;

        public EffectImpact(Effect effect)
        {
            this.effect = effect;
        }
        public string Description => "";
        public TextureRegion2D TextureRegion => throw new NotImplementedException();
        
        public void On(object target)
        {
            var character = target as Character;
            if (character is null)
                throw new ArgumentNullException(nameof(target));

            character.Effects.Add(effect);
        }
    }
}