using Microsoft.Xna.Framework;

namespace Askalhorn.Common.Geography.Local.Spawners
{
    internal abstract class BuildSpawner: ISpawner
    {
        protected Point RandomPoint(Location location)
        {
            var x = Storage.Random.Next(0, location.TiledMap.Width);
            var y = Storage.Random.Next(0, location.TiledMap.Height);

            var point = new Point(x, y);
            if (location.FreeForBuild(point))
                return point;
            return RandomPoint(location);
        }

        public abstract void Initialize(Location location);
    }
}