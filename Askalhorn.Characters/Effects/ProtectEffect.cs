using Askalhorn.Combat;
using Askalhorn.Common;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Characters.Effects
{
    internal class ProtectEffect: Effect
    {
        public DamageTypes Type { get; set; }

        public int Value { get; set; }
        
        public ProtectEffect(uint time) : base(time)
        {
        }

        public override string Description => $"Защищает от {Value} единиц урона {Type} в течении {TurnCount} ходов.";
        
        public override TextureRegion2D TextureRegion => Storage.Load("effects", 0, 0);

        public override void Subscribe(Character character)
        {
            base.Subscribe(character);

            character.Protection[Type].Addition.Value += Value;
        }

        public override void Unsubscribe(Character character)
        {
            base.Unsubscribe(character);

            character.Protection[Type].Addition.Value -= Value;
        }
    }
}