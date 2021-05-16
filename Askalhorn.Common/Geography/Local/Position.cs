using Microsoft.Xna.Framework;

namespace Askalhorn.Common.Geography.Local
{
    internal class Position: IPosition
    {
        public Point Point { get; set; }

        public int X => Point.X;

        public int Y => Point.Y;
        
        public Position(int x, int y)
        {
            Point = new Point(x, y);
        }

        private static readonly Vector2 xi = new Vector2(32, 16);

        private static readonly Vector2 yi = new Vector2(-32, 16);

        private static readonly Vector2 Origin = new Vector2(32, 32);

        public Vector2 RenderVector => Point.X * xi + Point.Y * yi - Origin;
    }
}
