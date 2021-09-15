using System;
using Askalhorn.Common;
using MonoGame.Extended.TextureAtlases;
using Serilog;

namespace Askalhorn.Characters.Impacts
{
    public class AddLevelEnergyImpact: IImpact
    {
        public readonly int Value;

        public AddLevelEnergyImpact(int value)
        {
            this.Value = value;
        }

        public string Description => "";
        public TextureRegion2D TextureRegion => throw new NotImplementedException();

        void IImpact.On(object target)
        {
            var character = target as Character;
            if (character is null)
                throw new ArgumentNullException(nameof(target));
            
            character.Level.AddEnergy(Value);
            Log.Information("{Name} получил {Value} единиц энергии развития", character.Name, Value);
        }
        
    }
}