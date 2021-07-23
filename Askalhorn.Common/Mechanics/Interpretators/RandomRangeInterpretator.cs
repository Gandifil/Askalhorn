using System;
using MonoGame.Extended;

namespace Askalhorn.Common.Mechanics.Interpretators
{
    internal class RandomRangeInterpretator: IInterpretator
    {
        public IInterpretator First { get; set; }
        
        public IInterpretator Second { get; set; }

        public override string ToString()
        {
            return $"{First}..{Second}";
        }

        public float Calculate(Character character)
        {
            var random = new Random();
            return random.NextSingle(First.Calculate(character), Second.Calculate(character));
        }
    }
}