using System;
using Askalhorn.Common.Geography.Local.Builds;
using Microsoft.Xna.Framework;

namespace Askalhorn.Common.Geography.Local.Spawners
{
    internal class TeleportSpawner: ISpawner
    {
        public void Initialize(Location location, Random random, int[] args, uint placeIndex)
        {
            var start = RandomPoint(location);
            var end = RandomPoint(location);
            location.AddBuild(new LocalTeleport()
            {
                Position = new Position(start),
                Target = end,
            });
        }

        private Point RandomPoint(Location location)
        {
            var x = Storage.Random.Next(0, location.TiledMap.Width);
            var y = Storage.Random.Next(0, location.TiledMap.Height);

            var point = new Point(x, y);
            if (location.FreeForBuild(point))
                return point;
            return RandomPoint(location);
        }
    }
}