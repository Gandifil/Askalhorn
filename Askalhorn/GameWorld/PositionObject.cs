using AmbrosiaGame.Utils;
using Microsoft.Xna.Framework;

namespace AmbrosiaGame.GameWorld
{
    abstract class PositionObject: GameObject
    {
        public PositionObject(int x, int y)
        {
            Position = new Point(x, y);
        }

        public Point Position { get; set; }

        public void Move(Point shift)
        {
            Position += shift;
            GameLog.Write("Object has been moved " + Position.ToString());
        }

        public static readonly Vector2 xi = new Vector2(32, 16);

        public static readonly Vector2 yi = new Vector2(-32, 16);

        public Vector2 GetWorldPosition => Position.X * xi + Position.Y * yi;
    }
}
