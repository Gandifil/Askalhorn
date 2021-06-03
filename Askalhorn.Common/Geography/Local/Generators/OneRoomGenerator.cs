using System;

namespace Askalhorn.Common.Geography.Local.Generators
{
    internal class OneRoomGenerator: ILocationGenerator
    {
        protected readonly uint Width;

        protected readonly uint Height;

        public OneRoomGenerator(uint width, uint height)
        {
            this.Width = width;
            this.Height = height;
        }
        
        public virtual ILocationGenerator.CellType[,] Create(Random random)
        {
            var cells = new ILocationGenerator.CellType[Width, Height];

            for (int x = 0; x < Width; x++)
            for (int y = 0; y < Height; y++)
                if (x == 0 || x == Width - 1 || y == 0 || y == Height - 1)
                    cells[x, y] = ILocationGenerator.CellType.Wall;
                else
                    cells[x, y] = ILocationGenerator.CellType.Floor;

            return cells;
        }
    }
}