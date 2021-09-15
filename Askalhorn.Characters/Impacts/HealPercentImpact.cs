using System;
using Askalhorn.Common;
using MonoGame.Extended.TextureAtlases;
using Serilog;

namespace Askalhorn.Characters.Impacts
{
    internal class HealPercentImpact: IImpact
    {
        public int Value { get; set; }
        
        public string Description => $"Восстанавливает {Value}% здоровья";
        
        public TextureRegion2D TextureRegion => Storage.Load("effects", 0, 0);

        void IImpact.On(object target)
        {
            var character = target as Character;
            if (character is null)
                throw new ArgumentNullException(nameof(target));

            var increment = character.HP.Current.Value * Value / 100;
            character.HP.Current.Value += increment;
            Log.Information("{Name} восстановил {increment} единиц здоровья", character.Name, increment);
        }
    }
}