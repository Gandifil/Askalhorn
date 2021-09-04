using System;
using Askalhorn.Common.Characters;
using Askalhorn.Common.Geography.Local.Spawners.PositionGenerators;
using Askalhorn.Common.Interpetators;
using Newtonsoft.Json;

namespace Askalhorn.Common.Geography.Local.Spawners
{
    internal class ContentCharacterSpawner: ISpawner
    {
        public IExpression<Position> PositionExpression { get;}

        public string Name { get;}

        [JsonConstructor]
        public ContentCharacterSpawner(IExpression<Position> positionExpression, string name = "ghost")
        {
            PositionExpression = positionExpression;
            Name = name;
        }
        
        public void Initialize(Location location, Random random, int[] args, uint placeIndex)
        {
            var position = PositionExpression.Generate(new ExpressionArgs
            {
                Location = location,
                Random = random,
            });
            
            var character = new ContentCharacter(Name)
            {
                Position = position,
            };
            
            Common.World.Instance.Add(character);
        }
    }
}