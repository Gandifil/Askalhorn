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
        
        public void On(Character character)
        {
            character.Level.AddEnergy(Value);
            Log.Information("{Name} получил {Value} единиц энергии развития", character.Name, Value);
        }
        
    }
}