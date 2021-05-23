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
        
        public ICell this[Point point] => Cells[point.X, point.Y];
        
        public ICell this[IPosition position] => this[position.X, position.Y];

        public Cell[,] Cells { get; private set; }

        IReadOnlyCollection<IBuild> ILocation.Builds => Builds;
        public bool Contain(IPosition position)
        {
            return position.X > 0
                   && position.Y > 0
                   && position.X < TiledMap.Width
                   && position.Y < TiledMap.Height;
        }

        void ILocation.AddCharacter(Character character)
        {
            throw new NotImplementedException();
        }

        public List<IBuild> Builds { get; private set; } = new List<IBuild>();

        public ManagedLocation(uint width, uint height)
        {
            Cells = new Cell[width, height];
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    Cells[x, y] = new Cell();
            
            TiledMap = new TiledMap("Global World", 
                (int)width, (int)height,
            64, 32, 
                TiledMapTileDrawOrder.RightDown, TiledMapOrientation.Isometric);
            TiledMap.AddLayer(new TiledMapTileLayer("floors", (int)width, (int)height, 64, 32));
            TiledMap.AddLayer(new TiledMapTileLayer("walls", 
                (int)width, (int)height, 64, 32, new Vector2(0, -32)));
            
            //var tiles = Storage.Content.Load<TiledMapTileset>("maps/grassland_tiles");
            //TiledMap.AddTileset(tiles, 0);
            
        }

        public void SetWall(uint x, uint y)
        {
            Cells[x, y].IsWall = true;
        }
        
        public void AddBuild<T>(int x, int y, T build) where T:HasPosition, IBuild, new()
        {
            build.Position = new Position(x, y);
            Builds.Add(build);
            Cells[x, y].Build = build;
        }
    }
}