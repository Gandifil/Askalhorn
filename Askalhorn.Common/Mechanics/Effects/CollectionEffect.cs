using System.Collections.Generic;
using System.Linq;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Common.Mechanics.Effects
{
    public class CollectionEffect: Effect
    {
        public List<Effect> Effects { get; set; } = new();
        
        public CollectionEffect(uint time) : base(time)
        {
        }

        public override string Description => string.Join(" ", Effects.Select(x => x.Description));
        public override TextureRegion2D TextureRegion => Effects[0].TextureRegion;

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