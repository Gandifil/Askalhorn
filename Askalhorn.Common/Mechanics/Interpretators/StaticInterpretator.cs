namespace Askalhorn.Common.Mechanics.Interpretators
{
    internal class StaticInterpretator: IInterpretator
    {
        public float Value { get; set; }

        public string Description => Value.ToString();

        public override string ToString()
        {
            return Value.ToString();
        }
        
        public float Calculate(Character character)
        {
            return Value;
        }
    }
}