using System;
using MonoGame.Extended.Tiled;

namespace Askalhorn.Common.Geography.Local.Designers
{
    class WhiteCastleDesigner: ILocationDesigner
    {
        public Location FormLocation(Random random, ref ILocationGenerator.CellType[,] map)
        {
            uint width = (uint)map.GetLength(0);
            uint height = (uint)map.GetLength(1);
            
            var location = new Location(width, height);
            var tiles = Storage.Content.Load<TiledMapTileset>("maps/dungeon_tiles");
            var grass_tiles = Storage.Content.Load<TiledMapTileset>("maps/grassland_tiles");
            location.TiledMap.AddTileset(grass_tiles, 0);
            location.TiledMap.AddTileset(tiles, 300);

            var floors = (TiledMapTileLayer) location.TiledMap.GetLayer("floors");
            var walls = (TiledMapTileLayer) location.TiledMap.GetLayer("walls");
                
            for (ushort x = 0; x < width; x++)
            for (ushort y = 0; y < height; y++)
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