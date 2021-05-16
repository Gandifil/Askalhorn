namespace Askalhorn.Common.Geography.Local
{
    class Chunk
    {
        public const int WIDTH = 256;

        public const int HEIGHT = 256;

        public Cell[,] Cells;

        public Cell this[int x, int y] => Cells[x, y];
    }
}
