using System;
using Askalhorn.Common.Characters;
using Askalhorn.Common.Geography.Local.Spawners.PositionGenerators;

namespace Askalhorn.Common.Geography.Local.Spawners
{
    internal class ContentCharacterSpawner: ISpawner
    {
        public IPositionGenerator PositionGenerator { get; set; } = new RandomGenerator();

        public string Name { get; set; } = "ghost";
        
        public void Initialize(Location location, Random random, int[] args, uint placeIndex)
        {
            var character = new ContentCharacter(Name)
            {
                Position = PositionGenerator.Generate(location, random),
            };
            Common.World.Instance.Add(character);
        }
    }
}