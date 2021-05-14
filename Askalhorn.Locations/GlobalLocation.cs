using Askalhorn.Locations.Local;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Tiled;
using System;

namespace Askalhorn.Locations
{
    public class GlobalLocation : ILocation
    {
        public TiledMap Map { get; private set; }

        private Chunk[,] Chunks = new Chunk[3, 3];

        public GlobalLocation()
        {
            Map = new TiledMap("Global World", 0, 0, 64, 32, TiledMapTileDrawOrder.LeftDown, TiledMapOrientation.Isometric);

        }

        private void Clear()
        {
            //var tile = new TiledMapTileObject();
            var layer = new TiledMapObjectLayer("", null);
            Map.AddLayer(layer);
            //layer.Objects.
        }

        public void PlayerMoved(Vector2 point)
        {
            throw new NotImplementedException();
        }
    }
}
