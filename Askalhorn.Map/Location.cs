using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Askalhorn.Common;
using Askalhorn.Map.Builds;
using Askalhorn.Map.Local;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Tiled;

namespace Askalhorn.Map
{
    public class Location: ILocation
    {
        public TiledMap TiledMap { get; }

        public readonly Dictionary<string, IPosition> Labels = new();

        ICell ILocation.this[IPosition position] => Cells[position.X, position.Y];
        
        public Cell this[IPosition position] => this[position.Point];

        public Rectangle Rectangle => new Rectangle(0, 0, Cells.GetLength(0), Cells.GetLength(1));
        public Cell this[Point point] => Rectangle.Contains(point) 
                ? Cells[point.X, point.Y]
                : new Cell
                {
                    IsWall = true,
                };

        public IReadOnlyCollection<IBuild> Builds => GameObjects.Where(x => x is IBuild).Select(x => x as IBuild).ToList();

        public Cell[,] Cells { get; private set; }

        public bool Contain(IPosition position)
        {
            return position.X > 0
                   && position.Y > 0
                   && position.X < TiledMap.Width
                   && position.Y < TiledMap.Height;
        }

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
        
        public void Add(IGameObject obj)
        {
            GameObjects.Insert(0, obj);
            this[obj.Position].Set(obj);
            obj.OnMoved += GameObjectMoved;
            obj.OnDisposed += Remove;
        }

        private void GameObjectMoved(IGameObject obj, IPosition from, IPosition to)
        {
            this[from].Remove(obj);
            this[to].Set(obj);
        }

        private void Remove(IGameObject obj)
        {
            GameObjects.Remove(obj);
            this[obj.Position].Remove(obj);
        }

        IReadOnlyCollection<IGameObject> ILocation.GameObjects => GameObjects;

        public List<IGameObject> GameObjects { get; } = new();

        public IPosition FindFreeSpaceForBuild(IPosition position)
        {
            if (FreeForBuild(position.Point))
                return position;
            
            for (uint x = position.X - 1; x <= position.X + 1; x++)
            for (uint y = position.Y - 1; y <= position.Y + 1; y++)
            {
                if (x == position.X && y == position.Y)
                    continue;

                var pos = new Position(x, y);
                if (FreeForBuild(pos.Point))
                    return pos;
            }

            throw new ArgumentException("Can't find near free space");
        }

        public IGameObject FindNear(IPosition position, Func<IGameObject, bool> p)
        {
            for (int x = (int)position.X - 1; x <= position.X + 1; x++)
            for (int y = (int)position.Y - 1; y <= position.Y + 1; y++)
            {
                if (x == position.X && y == position.Y)
                    continue;
                
                var cell = this[new Point(x, y)];
                if (cell.Build is not null && p(cell.Build))
                    return cell.Build;

                if (cell.DynamicObject is not null && p(cell.DynamicObject))
                    return cell.DynamicObject;
            }

            return null;
        }

        public static LocationKeeper Current = new LocationKeeper();
    }
}