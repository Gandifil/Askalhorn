using Serilog;

namespace Askalhorn.Common.Mechanics.Impacts
{
    internal class DamageImpact: IImpact
    {
        public readonly int Value;

        public DamageImpact(int value)
        {
            this.Value = value;
        }
        
        public void On(Character character)
        {
            character.HP.Current.Value -= Value;
            Log.Information("{Name} получил {Value} единиц урона", character.Name, Value);
        }
    }
}