using System;
using Askalhorn.Common.Interpetators;

namespace Askalhorn.Map.Local
{
    public class LabelPosition: IExpression<Position>
    {
        public string Name { get; }

        public LabelPosition(string name)
        {
            Name = name;
        }
        
        public Position Generate(object target, Random random)
        {
            var location = target as Location;
            if (location is null)
                throw new ArgumentOutOfRangeException(nameof(target), "Target must be Location");

            return location.Labels[Name] as Position;
        }
    }
}