using Microsoft.Xna.Framework;

namespace Askalhorn.Common.Geography.Local
{
    /// <summary>
    /// IPosition implementation via <see cref="T:Microsoft.Xna.Framework.Point" />
    /// </summary>
    public class Position: IPosition
    {
        public Point Point { get; set; }

        public uint X => (uint)Point.X;

        public uint Y => (uint)Point.Y;

        /// <summary>
        /// Create a position with the point (0, 0).
        /// </summary>
        public Position()
        {
            Point = new Point(0, 0);
        }
        
        /// <summary>
        /// Create a position with the point (x, y)
        /// </summary>
        /// <param name="x">The x coordinate in 2d-space tile map</param>
        /// <param name="y">The x coordinate in 2d-space tile map</param>
        public Position(int x, int y)
        {
            Point = new Point(x, y);
        }

        /// <summary>
        /// Create a position with some point.
        /// </summary>
        /// <param name="point">The point in 2d-space tile map</param>
        public Position(Point point)
        {
            Point = point;
        }
        
        /// <summary>
        /// Create a position copied from another IPosition.
        /// </summary>
        /// <param name="position">The position</param>
        public Position(IPosition position)
        {
            Point = position.Point;
        }

        private static readonly Vector2 xi = new Vector2(32, 16);

        private static readonly Vector2 yi = new Vector2(-32, 16);

        private static readonly Vector2 Origin = new Vector2(32, 32);

        private static readonly Vector2 ToCenter = new Vector2(0, 16);
        public Vector2 RenderTileVector => Point.X * xi + Point.Y * yi - Origin;
        
        public Vector2 RenderOriginVector => RenderVector + ToCenter;

        public Vector2 RenderVector => Point.X * xi + Point.Y * yi;
    }
}
