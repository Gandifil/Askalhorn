using Microsoft.Xna.Framework;

namespace Askalhorn.Common.Geography.Local
{
    public class Position
    {
        public Point Point { get; set; }

        public int X => Point.X;

        public int Y => Point.Y;
        
        public Position(int x, int y)
        {
            Point = new Point(x, y);
        }

        public void Move(Point shift)
        {
            Point += shift;
            //GameLog.Write("Object has been moved " + Position.ToString());
        }

        private static readonly Vector2 xi = new Vector2(32, 16);

        private static readonly Vector2 yi = new Vector2(-32, 16);

        public Vector2 RenderVector => Point.X * xi + Point.Y * yi;
    }
}
