using System;
using System.Linq;
using Askalhorn.Common.Geography.Local;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Tiled;

namespace Askalhorn.Common.Geography
{
    internal class ManagedLocation: ILocation
    {
        public TiledMap TiledMap { get; set; }

        public ICell this[uint x, uint y] => throw new System.NotImplementedException();

        public ManagedLocation()
        {
            TiledMap = new TiledMap("Global World", 50, 50, 64, 32, TiledMapTileDrawOrder.RightDown, TiledMapOrientation.Isometric);
            var tiles = Storage.Content.Load<TiledMapTileset>("maps/grassland_tiles");
            TiledMap.AddTileset(tiles, 0);
            
            var layer = new TiledMapTileLayer("", 50, 50, 64, 32);
            var random = new Random();
            for (ushort x = 0; x < 50; x++)
            {
                for (ushort y = 0; y < 50; y++)
                {
                    layer.SetTile(x, y, (uint)(TiledMap.GetTilesetFirstGlobalIdentifier(tiles) + (random.Next() % 20)));
                }
            }
            
            
            TiledMap.AddLayer(layer);
            
            
        }
    }
}