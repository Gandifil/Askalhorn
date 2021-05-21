using System;
using System.Collections.Generic;
using System.Linq;
using Askalhorn.Common.Geography.Local;
using Askalhorn.Common.Geography.Local.Builds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;

namespace Askalhorn.Common.Geography
{
    internal class ManagedLocation: ILocation
    {
        public TiledMap TiledMap { get; set; }

        public ICell this[uint x, uint y] => Cells[x, y];

        public Cell[,] Cells { get; private set; } = new Cell[50, 50];

        IReadOnlyCollection<IBuild> ILocation.Builds => Builds;
        public List<IBuild> Builds { get; private set; } = new List<IBuild>();

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

            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    Cells[x, y] = new Cell();
                }
            }
            
            TiledMap.AddLayer(layer);

            AddBuild(0, 0, new LocalTeleport()
            {
                Shift = new Point(10, 10),
            });
        }
        
        protected void AddBuild<T>(int x, int y, T build) where T:HasPosition, IBuild, new()
        {
            build.Position = new Position(x, y);
            Builds.Add(build);
            Cells[x, y].Build = build;
        }
    }
}