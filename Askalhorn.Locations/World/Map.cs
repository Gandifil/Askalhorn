using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Askalhorn.Locations.World
{
    public struct Map
    {
        public Cell[,] Cells;

        public Cell this[int x, int y] => Cells[x, y];

        public int Width => Cells.GetLength(0);

        public int Height => Cells.GetLength(1);

        public Map(uint width, uint height)
        {
            Cells = new Cell[width, height];
        }
    }
}
