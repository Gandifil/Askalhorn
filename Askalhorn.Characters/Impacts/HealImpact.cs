using System;
using Askalhorn.Common;
using MonoGame.Extended.TextureAtlases;
using Newtonsoft.Json;
using Serilog;

namespace Askalhorn.Characters.Impacts
{
    public class HealImpact: IImpact
    {
        public int Value { get; }

        [JsonConstructor]
        public HealImpact(int value)
        {
            Value = value;
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