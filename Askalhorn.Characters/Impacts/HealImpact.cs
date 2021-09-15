using System;
using Askalhorn.Common;
using MonoGame.Extended.TextureAtlases;
using Serilog;

namespace Askalhorn.Characters.Impacts
{
    public class HealImpact: IImpact
    {
        public readonly int Value;

        public HealImpact(int value)
        {
            this.Value = value;
        }
        public string Description => "";
        public TextureRegion2D TextureRegion => Storage.Load("effects", 0, 0);
        
        public void On(object target)
        {
            var character = target as Character;
            if (character is null)
                throw new ArgumentNullException(nameof(target));

            character.HP.Current.Value += Value;
            Log.Information("{Name} восстановил {Value} единиц здоровья", character.Name, Value);
        }
    }
}