using System;
using Microsoft.Xna.Framework;

namespace Askalhorn.Common.Geography.Local.Spawners
{
    internal class CustomBuildSpawner: BuildSpawner
    {
        private readonly Func<Point, Random, int[], uint, IBuild> F;

        public CustomBuildSpawner(Func<Point, Random, int[], uint, IBuild> F)
        {
            this.F = F;
        }
        
        public override void Initialize(Location location, Random random, int[] args, uint placeIndex)
        {
            var buld = F?.Invoke(RandomPoint(location, random),random, args, placeIndex);
            location.AddBuild(buld);
        }
    }
}