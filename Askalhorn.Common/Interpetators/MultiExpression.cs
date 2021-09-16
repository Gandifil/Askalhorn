using System;

namespace Askalhorn.Common.Interpetators
{
    public class MultiExpression<T>: IExpression<T>
    {
        public IExpression<T> First { get; set; }
        
        public IExpression<T> Second { get; set; }

        public override string ToString()
        {
            return $"{First}*{Second}";
        }
        
        public T Generate(ExpressionArgs args)
        {
            var a = First.Generate(args);
            var b = Second.Generate(args);
            
            var tuple = new Tuple<T, T>(a, b);
            switch (tuple)
            {
                case Tuple<int, int> pair:
                    return (T)(object) (pair.Item1 * pair.Item2);
                
                case Tuple<uint, uint> pair:
                    return (T)(object) (pair.Item1 * pair.Item2);
                
                case Tuple<float, float> pair:
                    return (T)(object) (pair.Item1 * pair.Item2);
                
                default:
                    throw new ArgumentOutOfRangeException("T can't be type " + typeof(T).Name);
            }
        }
    }
}