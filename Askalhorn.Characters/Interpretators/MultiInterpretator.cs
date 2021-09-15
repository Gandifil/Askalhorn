namespace Askalhorn.Characters.Interpretators
{
    internal class MultiInterpretator: IInterpretator
    {
        public IInterpretator First { get; set; }
        
        public IInterpretator Second { get; set; }

        public override string ToString()
        {
            return $"{First}*{Second}";
        }
        
        public float Calculate(Character character)
        {
            return First.Calculate(character) * Second.Calculate(character);
        }
    }
}