using System;
using Askalhorn.Common.Characters;

namespace Askalhorn.Common.Geography.Local.Spawners
{
    internal class WitchSpawner: BuildSpawner
    {
        public override void Initialize(Location location, Random random, int[] args, uint placeIndex)
        {
            var witch = new Witch()
            {
                Position = new Position(RandomPoint(location, random)),
            };
            Common.World.Instance.Add(witch);
        }
    }
}