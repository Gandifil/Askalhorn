using System;
using Microsoft.Xna.Framework;

namespace Askalhorn.Map.Local.Spawners
{
    public abstract class BuildSpawner: ISpawner
    {
        protected Point RandomPoint(Location location, Random random)
        {
            var x = random.Next(0, location.TiledMap.Width);
            var y = random.Next(0, location.TiledMap.Height);

            var point = new Point(x, y);
            if (location.FreeForBuild(point))
                return point;
            return RandomPoint(location, random);
        }

        public abstract void Initialize(Location location, Random random, int[] args, uint placeIndex);
    }
}