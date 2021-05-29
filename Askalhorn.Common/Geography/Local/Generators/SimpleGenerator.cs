using Askalhorn.Common.Geography.Local.Builds;
using Askalhorn.Common.Maths;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Tiled;

namespace Askalhorn.Common.Geography.Local.Generators
{
    class SimpleGenerator: IManagedLocationGenerator
    {
        public ManagedLocation Location
        {
            get
            {
                var location = new ManagedLocation(50, 50);

                var labirint = new LabirintGenerator(50, 50);
                var cells = labirint.Create(10, 10);
                
                var tiles = Storage.Content.Load<TiledMapTileset>("maps/dungeon_tiles");
                var grass_tiles = Storage.Content.Load<TiledMapTileset>("maps/grassland_tiles");
                location.TiledMap.AddTileset(grass_tiles, 0);
                location.TiledMap.AddTileset(tiles, 300);

                var floors = (TiledMapTileLayer) location.TiledMap.GetLayer("floors");
                var walls = (TiledMapTileLayer) location.TiledMap.GetLayer("walls");
                
                for (ushort x = 0; x < 50; x++)
                for (ushort y = 0; y < 50; y++)
                    floors.SetTile(x, y,  0);
                
                for (ushort x = 0; x < 50; x++)
                    for (ushort y = 0; y < 50; y++)
                        if (cells[x, y] == LabirintGenerator.CellType.Wall)
                        {
                            walls.SetTile(x, y, 
                                (uint)(location.TiledMap.GetTilesetFirstGlobalIdentifier(tiles) + 32));
                            location.SetWall(x, y); 
                        }
                
                
                location.AddBuild(0, 0, new LocalTeleport()
                {
                    Shift = new Point(10, 10),
                });

                location.AddBuild(10, 10, new GlobalTeleport()
                {
                    Shift = new Point(20, 20),
                });

                location.AddBuild(2, 2, new Chest());
                
                return location;
            }
        }
    }
}