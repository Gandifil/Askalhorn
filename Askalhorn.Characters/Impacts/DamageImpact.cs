using System;
using Askalhorn.Common;
using MonoGame.Extended.TextureAtlases;
using Serilog;

namespace Askalhorn.Characters.Impacts
{
    public class DamageImpact: IImpact
    {
        public readonly int Value;

        public DamageImpact(int value)
        {
            this.Value = value;
        }
        public string Description => $"Нанесение {Value} единиц урона";
        public TextureRegion2D TextureRegion => Storage.Load("effects", 1, 0);
        
        public void On(object target)
        {
            var character = target as Character;
            if (character is null)
                throw new ArgumentNullException(nameof(target));
            
            character.HP.Current.Value -= Value;
            Log.Information("{Name} получил {Value} единиц урона", character.Name, Value);
        }
    }
}