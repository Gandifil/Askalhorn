using System;
using Microsoft.Xna.Framework;
using Serilog;

namespace Askalhorn.Common.Maths
{
    /// <summary>
    /// Rhombus describe a shape with space position which has size and center.
    /// </summary>
    public class Rhombus
    {
        public int Width { get; set; }
        
        public int Height { get; set; }

        public Vector2 Center { get; set; }

        /// <summary>
        /// Check point or vector inside rhombus or not.
        /// </summary>
        /// <param name="p">Point</param>
        /// <returns>true if <paramref name="p"/> inside rhombus</returns>
        public bool Contains(Vector2 p)
        {
            var r = p - Center;

            r.X = Math.Abs(r.X);
            r.Y = Math.Abs(r.Y);
            

            var a = Width / 2;
            var b = Height / 2;

            return r.X / a + r.Y / b <= 1f;
        }
    }
}