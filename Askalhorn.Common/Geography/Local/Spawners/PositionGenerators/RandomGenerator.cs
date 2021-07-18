using System;
using Microsoft.Xna.Framework;

namespace Askalhorn.Common.Geography.Local.Spawners.PositionGenerators
{
    internal class RandomGenerator: IPositionGenerator
    {
        public Position Generate(Location location, Random random)
        {
            var x = random.Next(0, location.TiledMap.Width);
            var y = random.Next(0, location.TiledMap.Height);

            var point = new Point(x, y);
            if (location.FreeForBuild(point))
                return new Position(point);
            return Generate(location, random);
        }
    }
}