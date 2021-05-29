using Serilog;

namespace Askalhorn.Common.Mechanics.Impacts
{
    internal class HealImpact: IImpact
    {
        public readonly int Value;

        public HealImpact(int value)
        {
            this.Value = value;
        }
        
        public void On(Character character)
        {
            character.HP.Current.Value += Value;
            Log.Information("{Name} восстановил {Value} единиц здоровья", character.Name, Value);
        }
    }
}