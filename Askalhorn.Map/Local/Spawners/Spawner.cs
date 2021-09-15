using System;
using Askalhorn.Common.Interpetators;
using Newtonsoft.Json;

namespace Askalhorn.Map.Local.Spawners
{
    internal class Spawner: ISpawner
    {
        public IExpression<Position> PositionExpression { get;}

        public IGameObjectBuilder Builder { get;}

        [JsonConstructor]
        public Spawner(IExpression<Position> positionExpression, IGameObjectBuilder builder)
        {
            PositionExpression = positionExpression;
            Builder = builder;
        }
        
        public void Initialize(Location location, Random random, int[] args, uint placeIndex)
        {
            Location.Current.Location.GameObjects.Add(
                Builder.Build(PositionExpression.Generate(new LocationExpressionArgs
            {
                Location = location,
                Random = random,
            })));
        }
    }
}