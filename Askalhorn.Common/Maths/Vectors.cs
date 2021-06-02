using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace Askalhorn.Common.Maths
{
    public static class Vectors
    {
        public static readonly Vector2 xi = new Vector2(32, 16);

        public static readonly Vector2 yi = new Vector2(-32, 16);

        public static readonly Vector2 Origin = new Vector2(32, 32);

        public static readonly Vector2 SmallOrigin = new Vector2(32, 0);

        public static readonly Vector2 ToCenter = new Vector2(0, 16);

        public static Vector2 Transform(Point point)
        {
            return point.X * Vectors.xi + point.Y * Vectors.yi;
        }

        public static Point Detransform(Vector2 vector)
        {
            var sum = vector.Y / 16; // = x + y
            var add = vector.X / 32; // = x - y

            var px = sum + add; // = 2*x
            var py = sum - add; // = 2*y
            
            
            return new Point((int)Math.Floor(px / 2), (int)Math.Floor(py / 2));
        }
    }
}