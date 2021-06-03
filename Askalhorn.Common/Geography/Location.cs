using System.Collections.Generic;
using Askalhorn.Common.Geography.Local;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Content;
using MonoGame.Extended.Tiled;

namespace Askalhorn.Common.Geography
{
    internal class Location: ILocation
    {
        public TiledMap TiledMap { get; set; }
        
        ICell ILocation.this[IPosition position] => Cells[position.X, position.Y];
        
        public Cell this[IPosition position] => Cells[position.X, position.Y];

        public Cell[,] Cells { get; private set; }

        IReadOnlyCollection<IBuild> ILocation.Builds => Builds;
        public bool Contain(IPosition position)
        {
            return position.X > 0
                   && position.Y > 0
                   && position.X < TiledMap.Width
                   && position.Y < TiledMap.Height;
        }

        public List<IBuild> Builds { get; private set; } = new List<IBuild>();

        public Location(uint width, uint height)
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
            TiledMap.AddLayer(new TiledMapTileLayer("walls",  (int)width, (int)height, 64, 32, new Vector2(0, -32)));
        }

        public Location(string name)
        {
            TiledMap = Storage.Content.Load<TiledMap>("maps/" + name);
            
            Cells = new Cell[TiledMap.Width, TiledMap.Height];
            for (int x = 0; x < TiledMap.Width; x++)
            for (int y = 0; y < TiledMap.Height; y++)
                Cells[x, y] = new Cell();
        }

        public bool FreeForBuild(Point point)
        {
            var position = new Position(point);
            return Contain(position) && !this[position].IsWall && this[position].Build is null;
        }

        public void SetWall(uint x, uint y)
        {
            Cells[x, y].IsWall = true;
        }
        
        public void AddBuild(IBuild build)
        {
            Builds.Add(build);
            this[build.Position].Build = build;
        }
        
    }
}