using System;
using Askalhorn.Common;
using Askalhorn.Render;
using MonoGame.Extended.TextureAtlases;
using Newtonsoft.Json;

namespace Askalhorn.Characters.Effects
{
    public class ImpactEffect: IEffect
    {
        public readonly IImpact Impact;
        
        public ImpactEffect(IImpact impact)
        {
            Impact = impact;
        }

        public void Turn(Character character)
        {
            Impact.On(character);
        }

        [JsonIgnore]
        public string Description => Impact.Description + " каждый ход";

        [JsonIgnore]
        public string TooltipText => Impact.Description + " каждый ход";
        public event Action OnChanged;
        
        [JsonIgnore]
        public TextureRenderer Renderer => new(Impact.TextureRegion);
    }
}