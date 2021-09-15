using System;
using Askalhorn.Common;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Tiled;

namespace Askalhorn.Map.Local.Designers
{
    class WhiteCastleDesigner: ILocationDesigner
    {
        public Location FormLocation(Random random, ref ILocationGenerator.CellType[,] map)
        {
            uint width = (uint)map.GetLength(0);
            uint height = (uint)map.GetLength(1);
            
            var location = new Location(width, height);
            
            var tiles = Storage.Content.Load<TiledMapTileset>("maps/dungeon_tiles");

            var floors = new TiledMapTileLayer("floors", (int) width, (int) height, 64, 32, new Vector2(0, -32));
            location.TiledMap.AddLayer(floors);

            var walls = new TiledMapTileLayer("walls", (int) width, (int) height, 64, 32, new Vector2(0, -32));
            location.TiledMap.AddLayer(walls);
            
            location.TiledMap.AddTileset(tiles, 1);

            for (ushort x = 1; x < width; x++)
            for (ushort y = 1; y < height; y++)
                floors.SetTile(x, y,  (uint)(location.TiledMap.GetTilesetFirstGlobalIdentifier(tiles) + 74));
                
            for (ushort x = 0; x < width; x++)
            for (ushort y = 0; y < height; y++)
                if (map[x, y] == ILocationGenerator.CellType.Wall)
                {
                    walls.SetTile(x, y, 
                        (uint)(location.TiledMap.GetTilesetFirstGlobalIdentifier(tiles) + 32));
                    location.SetWall(x, y); 
                }
                
            return location;
            
        }
    }
}