using System;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace Askalhorn.Map.Generators
{
    internal class RoomsAndCorridorsGenerator: ILocationGenerator
    {
        protected readonly int Width;

        protected readonly int Height;

        public Size MinRoom { get; set; } = new Size(5, 5);
        public Size MaxRoom { get; set; } = new Size(5, 5);

        public RoomsAndCorridorsGenerator(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }
        
        public ILocationGenerator.CellType[,] Create(Random random, out Point[] places)
        {
            var cells = new ILocationGenerator.CellType[Width, Height];

            for (int x = 0; x < Width; x++)
            for (int y = 0; y < Height; y++)
                cells[x, y] = ILocationGenerator.CellType.Wall;
            
            Rectangle[] rooms = new Rectangle[10];
            for (int i = 0; i < 10; i++)
            {
                var room = gen(random);
                rooms[i] = room;
                for (int x = room.Left; x < room.Right; x++)
                for (int y = room.Top; y < room.Bottom; y++)
                    cells[x, y] = ILocationGenerator.CellType.Floor;
            }

            for (int i = 0; i < 10; i++)
            {
                var startRoom = rooms[i];
                var endRoom = rooms[random.Next(0, rooms.Length)];
                
                var startPoint = new Point(
                    random.Next(startRoom.Left, startRoom.Right),
                    random.Next(startRoom.Top, startRoom.Bottom)
                );
                
                var endPoint = new Point(
                    random.Next(endRoom.Left, endRoom.Right),
                    random.Next(endRoom.Top, endRoom.Bottom)
                );
                
                for (int x = startPoint.X; x < endPoint.X; x++)
                    cells[x, startPoint.Y] = ILocationGenerator.CellType.Floor;
                    
                
                for (int y = startPoint.Y; y < endPoint.Y; y++)
                    cells[endPoint.X, y] = ILocationGenerator.CellType.Floor;
            }

            for (int i = 0; i < 10; i++)
            {
                var startRoom = rooms[random.Next(0, rooms.Length)];
                var endRoom = rooms[random.Next(0, rooms.Length)];
                
                var startPoint = new Point(
                    random.Next(startRoom.Left, startRoom.Right),
                    random.Next(startRoom.Top, startRoom.Bottom)
                );
                
                var endPoint = new Point(
                    random.Next(endRoom.Left, endRoom.Right),
                    random.Next(endRoom.Top, endRoom.Bottom)
                );
                
                for (int x = startPoint.X; x < endPoint.X; x++)
                    cells[x, startPoint.Y] = ILocationGenerator.CellType.Floor;
                    
                
                for (int y = startPoint.Y; y < endPoint.Y; y++)
                    cells[endPoint.X, y] = ILocationGenerator.CellType.Floor;
            }

            places = rooms.Select(x => x.Center).ToArray();

            return cells;
        }

        private Rectangle gen(Random random)
        {
            var pos = new Point(
                random.Next(1, Width - MinRoom.Width),
                random.Next(1, Height - MinRoom.Height)
                );
            
            var size = new Point(
                random.Next(MinRoom.Width, MaxRoom.Width),
                random.Next(MinRoom.Height, MaxRoom.Height));

            return new Rectangle(pos, size);
        }
        
    }
}