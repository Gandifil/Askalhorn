namespace Askalhorn.Common.Geography.World
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
