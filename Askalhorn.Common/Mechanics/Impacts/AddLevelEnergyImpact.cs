using System;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;
using Serilog;

namespace Askalhorn.Common.Mechanics.Impacts
{
    internal class AddLevelEnergyImpact: IImpact
    {
        public readonly int Value;

        public AddLevelEnergyImpact(int value)
        {
            this.Value = value;
        }

        public string Description => "";
        public TextureRegion2D TextureRegion => throw new NotImplementedException();

        public void On(Character character)
        {
            character.Level.AddEnergy(Value);
            Log.Information("{Name} получил {Value} единиц энергии развития", character.Name, Value);
        }
        
    }
}