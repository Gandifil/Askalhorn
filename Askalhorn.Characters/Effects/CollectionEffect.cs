using System;
using System.Collections.Generic;
using System.Linq;
using Askalhorn.Render;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Characters.Effects
{
    public class CollectionEffect: IEffect
    {
        public List<IEffect> Effects { get; set; } = new();

        public string TooltipText => string.Join(" ", Effects.Select(x => x.TooltipText));
        public event Action OnChanged;
        public TextureRenderer Renderer => Effects[0].Renderer;

        public void Subscribe(Character character)
        {
            foreach (var effect in Effects)
                effect.Subscribe(character);
        }

        public void Unsubscribe(Character character)
        {
            foreach (var effect in Effects)
                effect.Unsubscribe(character);
        }

        public void Turn(Character character)
        {
            foreach (var effect in Effects)
                effect.Turn(character);
        }
    }
}