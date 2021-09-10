using Newtonsoft.Json;

namespace Askalhorn.Common.Interpetators
{
    public class StaticExpression<T>: IExpression<T>
    {
        public readonly T Value;

        [JsonConstructor]
        public StaticExpression(T value)
        {
            Value = value;
        }
        
        public T Generate(ExpressionArgs args)
        {
            return Value;
        }
    }
}