using System;
using Askalhorn.Common.Characters;
using Askalhorn.Common.Control;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Askalhorn.Common.Geography.Local.Spawners
{
    internal class TestEnemySpawner: BuildSpawner
    {
        public override void Initialize(Location location, Random random, int[] args, uint placeIndex)
        {
            var character = new ContentCharacter("ghost")
            {
                Position = new Position(RandomPoint(location, random)),
            };
            Common.World.Instance.Add(character);
        }
    }
}