using Askalhorn.Common.Interpetators;
using Newtonsoft.Json;

namespace Askalhorn.Map.Local
{
    public class StaticPosition: IExpression<Position>
    {
        public uint X { get; }
        
        public uint Y { get;}

        [JsonConstructor]
        public StaticPosition(uint x, uint y)
        {
            X = x;
            Y = y;
        }
        
        public Position Generate(ExpressionArgs args)
        {
            return new (X, Y);
        }
    }
}