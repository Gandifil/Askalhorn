using System;
using Askalhorn.Combat;
using Askalhorn.Common;
using Askalhorn.Render;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Characters.Effects
{
    internal class ProtectEffect: IEffect
    {
        public DamageTypes Type { get; set; }

        public int Value { get; set; }

        public string TooltipText => $"Защищает от {Value} единиц урона {Type}";
        public event Action OnChanged;

        public TextureRenderer Renderer => new TextureRenderer("effects", new());

        public void Turn(Character character)
        {
        }

        public void Subscribe(Character character)
        {
            character.Protection[Type].Addition.Value += Value;
        }

        public void Unsubscribe(Character character)
        {
            character.Protection[Type].Addition.Value -= Value;
        }
    }
}