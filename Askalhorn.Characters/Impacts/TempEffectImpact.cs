using System;
using Askalhorn.Characters.Effects;
using Askalhorn.Common;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Characters.Impacts
{
    public class TempEffectImpact: IImpact
    {
        public readonly IEffect effect;

        public uint TurnCount { get; set; }

        public TempEffectImpact(IEffect effect, uint turnCount)
        {
            this.effect = effect;
            TurnCount = turnCount;
        }
        public string Description => "";
        public TextureRegion2D TextureRegion => throw new NotImplementedException();
        
        public void On(object target)
        {
            var character = target as Character;
            if (character is null)
                throw new ArgumentNullException(nameof(target));

            character.EffectPool.Add(new TempEffectBind(effect, TurnCount));
        }
    }
}