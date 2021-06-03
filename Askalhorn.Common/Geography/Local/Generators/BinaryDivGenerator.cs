using System;
using Microsoft.Xna.Framework;

namespace Askalhorn.Common.Geography.Local.Generators
{
    class LabirintGenerator: OneRoomGenerator
    {
        public uint MinWidth { get; set; } = 5;

        public uint MinHeight { get; set; } = 5;

        public LabirintGenerator(uint width, uint height)
            :base(width, height)
        {
        }

        public override ILocationGenerator.CellType[,] Create(Random random)
        {
            var cells = base.Create(random);
            
            Split(random, new Rectangle(1, 1, (int)Width - 2, (int)Height - 2), ref cells);

            return cells;
        }

        private void Split(Random random, Rectangle rect, ref ILocationGenerator.CellType[,] cells)
        {
            if (rect.Width > MinWidth && rect.Height > MinHeight)
            {
                var doorX = random.Next(rect.Left + (int)MinWidth / 2, rect.Right - (int)MinWidth / 2);
                var doorY = random.Next(rect.Top + (int)MinHeight / 2, rect.Bottom - (int)MinHeight / 2);

                if (rect.Width > rect.Height)
                {
                    // делим по ширине
                    for (int i = 0; i < rect.Height; i++)
                        cells[doorX, rect.Y + i] = rect.Y + i == doorY ? ILocationGenerator.CellType.Door : ILocationGenerator.CellType.Wall;

                    var rect1 = new Rectangle(rect.Location, rect.Size);
                    rect1.Width = (int)doorX - rect.X ;
                    
                    Split(random, rect1, ref cells);
                    rect1.X = (int)doorX + 1;
                    rect1.Width = rect.Width - (int)doorX;
                    Split(random, rect1, ref cells);
                }
                else
                {
                    // делим по высоте
                    for (int i = 0; i < rect.Width; i++)
                        cells[rect.X + i, doorY] = rect.X + i == doorX ? ILocationGenerator.CellType.Door : ILocationGenerator.CellType.Wall;

                    var rect1 = new Rectangle(rect.Location, rect.Size);
                    rect1.Height = (int)doorY - rect.Y;
                    Split(random, rect1, ref cells);
                    
                    rect1.Y = (int)doorY + 1;
                    rect1.Height = rect.Height - (int)doorY;
                    Split(random, rect1, ref cells);
                }
            }
        }
    }
}