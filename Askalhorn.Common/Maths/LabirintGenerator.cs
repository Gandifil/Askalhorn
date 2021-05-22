using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace Askalhorn.Common.Maths
{
    class LabirintGenerator
    {
        public enum CellType
        {
            Floor,
            Wall,
            Door,
        }

        private readonly uint Width;

        private readonly uint Height;

        private readonly Random random = new Random();

        public uint MinWidth { get; set; }

        public uint MinHeight { get; set; }

        public LabirintGenerator(uint width, uint height)
        {
            this.Width = width;
            this.Height = height;
        }
        
        public CellType[,] Create(uint minWidth, uint minHeight)
        {
            var cells = new CellType[Width, Height];

            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                    if (x == 0 || x == Width - 1 || y == 0 || y == Height - 1)
                        cells[x, y] = CellType.Wall;
                    else
                        cells[x, y] = CellType.Floor;
            MinWidth = minWidth;
            MinHeight = minHeight;
            
            Split(new Rectangle(1, 1, (int)Width - 2, (int)Height - 2), ref cells);

            return cells;
        }

        private void Split(Rectangle rect, ref CellType[,] cells)
        {
            if (rect.Width > MinWidth && rect.Height > MinHeight)
            {
                var doorX = random.Next(rect.Left + (int)MinWidth / 2, rect.Right - (int)MinWidth / 2);
                var doorY = random.Next(rect.Top + (int)MinHeight / 2, rect.Bottom - (int)MinHeight / 2);

                if (rect.Width > rect.Height)
                {
                    // делим по ширине
                    for (int i = 0; i < rect.Height; i++)
                        cells[doorX, rect.Y + i] = rect.Y + i == doorY ? CellType.Door : CellType.Wall;

                    var rect1 = new Rectangle(rect.Location, rect.Size);
                    rect1.Width = (int)doorX - rect.X - 1;
                    
                    Split(rect1, ref cells);
                    rect1.X = (int)doorX + 1;
                    rect1.Width = rect.Width - (int)doorX;
                    Split(rect1, ref cells);
                }
                else
                {
                    // делим по высоте
                    for (int i = 0; i < rect.Width; i++)
                        cells[rect.X + i, doorY] = rect.X + i == doorX ? CellType.Door : CellType.Wall;

                    var rect1 = new Rectangle(rect.Location, rect.Size);
                    rect1.Height = (int)doorY - rect.Y - 1;
                    
                    Split(rect1, ref cells);
                    rect1.Y = (int)doorY + 1;
                    rect1.Height = rect.Height - (int)doorY;
                    Split(rect1, ref cells);
                }
            }
        }
    }
}