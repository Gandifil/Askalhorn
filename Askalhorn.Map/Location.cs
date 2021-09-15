using System.Collections.Generic;
using Askalhorn.Common;
using Askalhorn.Map.Local;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Tiled;

namespace Askalhorn.Map
{
    public class Location: ILocation
    {
        public TiledMap TiledMap { get; set; }
        
        ICell ILocation.this[IPosition position] => Cells[position.X, position.Y];
        
        public Cell this[IPosition position] => Cells[position.X, position.Y];

        public readonly List<Position> Places = new();

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
        }

        public Location(string name)
        {
            TiledMap = Storage.Content.Load<TiledMap>("maps/" + name);
            
            Cells = new Cell[TiledMap.Width, TiledMap.Height];
            for (int x = 0; x < TiledMap.Width; x++)
            for (int y = 0; y < TiledMap.Height; y++)
                Cells[x, y] = new Cell();

            foreach (var layer in TiledMap.TileLayers)
            {
                if (layer.Name.Contains("walls"))
                    foreach (var tile in layer.Tiles)
                        SetWall(tile.X, tile.Y);
                
                if (layer.Name.StartsWith("64"))
                    layer.Offset = new Vector2(0, -32);
            }
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

        IReadOnlyCollection<IGameObject> ILocation.GameObjects => GameObjects;

        public List<IGameObject> GameObjects { get; } = new();

         public IGameObject Find(IPosition position)
         {
             foreach (var character in GameObjects)
                 if (character.Position.Point == position.Point)
                     return character;

             return null;
         }

         public IGameObject FindNear(IPosition position)
         {
             foreach (var character in GameObjects)
                 if (character.Position.Point != position.Point && character.Position.IsInside(position, 1.5f))
                     return character;

             return null;
         }

         public static LocationKeeper Current = new LocationKeeper();
    }
}