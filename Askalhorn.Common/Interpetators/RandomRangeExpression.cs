using System;
using MonoGame.Extended;

namespace Askalhorn.Common.Interpetators
{
    public class RandomRangeInterpretator<T>: IExpression<T>
    {
        public IExpression<T> First { get; set; }
        
        public IExpression<T> Second { get; set; }

        public override string ToString()
        {
            return $"{First}..{Second}";
        }

        public T Generate(object target, Random random)
        {
            var start = First.Generate(target, random);
            var end = Second.Generate(target, random);

            var tuple = new Tuple<T, T>(start, end);
            
            switch (tuple)
            {
                case Tuple<int, int> range:
                    return (T)(object) random.Next(range.Item1, range.Item2);
                
                case Tuple<uint, uint> range:
                    var value = (uint)random.Next((int)range.Item1, (int)range.Item2);
                    return (T) (object) value;
                
                case Tuple<float, float> range:
                    return (T)(object) random.NextSingle(range.Item1, range.Item2);
                
                default:
                    throw new ArgumentOutOfRangeException("T can't be type " + typeof(T).Name);
            }
        }
    }
}