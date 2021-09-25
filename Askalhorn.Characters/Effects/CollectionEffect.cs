using System.Collections.Generic;
using System.Linq;
using Askalhorn.Render;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Characters.Effects
{
    public class CollectionEffect: Effect
    {
        public List<Effect> Effects { get; set; } = new();
        
        public CollectionEffect(uint time) : base(time)
        {
        }

        public override string TooltipText => string.Join(" ", Effects.Select(x => x.TooltipText));
        public override TextureRenderer Renderer => Effects[0].Renderer;

        public override void Subscribe(Character character)
        {
            base.Subscribe(character);

            foreach (var effect in Effects)
                effect.Subscribe(character);
        }

        public override void Unsubscribe(Character character)
        {
            base.Unsubscribe(character);

            foreach (var effect in Effects)
                effect.Unsubscribe(character);
        }

        public override void Tick(Character character)
        {
            base.Tick(character);

            foreach (var effect in Effects)
                effect.Tick(character);
        }
    }
}