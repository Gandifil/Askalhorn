using System;
using Microsoft.Xna.Framework;

namespace Askalhorn.Common.Geography.Local.Spawners
{
    internal class CustomBuildSpawner: BuildSpawner
    {
        private readonly Func<Point, IBuild> F;

        public CustomBuildSpawner(Func<Point, IBuild> F)
        {
            this.F = F;
        }
        
        public override void Initialize(Location location)
        {
            location.AddBuild(F?.Invoke(RandomPoint(location)));
        }
    }
}