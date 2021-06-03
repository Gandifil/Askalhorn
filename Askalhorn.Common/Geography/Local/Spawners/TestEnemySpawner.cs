using System;
using Askalhorn.Common.Control;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Askalhorn.Common.Geography.Local.Spawners
{
    internal class TestEnemySpawner: ISpawner
    {
        public void Initialize(Location location, Random random, IPosition playerPosition)
        {
            var character = new Character()
            {
                Texture = Storage.Content.Load<Texture2D>("images/mage2"),
                Position = new Position(RandomPoint(location)),
            };
            
            character.Controller = new RandomMovementController(character);
            Common.World.Instance.Add(character);
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