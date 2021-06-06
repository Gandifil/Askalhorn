﻿using System.Text.Json.Serialization;
using Askalhorn.Common.Maths;
using Microsoft.Xna.Framework;

namespace Askalhorn.Common.Geography.Local
{
    /// <summary>
    /// IPosition implementation via <see cref="T:Microsoft.Xna.Framework.Point" />
    /// </summary>
    public class Position: IPosition
    {
        [JsonIgnore]
        public Point Point { get; set; }

        [JsonInclude]
        public uint X => (uint)Point.X;

        [JsonInclude]
        public uint Y => (uint)Point.Y;
        
        public IPosition Shift(Point direction)
        {
            return new Position(Point + direction);
        }

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
        [System.Text.Json.Serialization.JsonConstructor]
        public Position(uint x, uint y)
        {
            Point = new Point((int)x, (int)y);
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
        public Vector2 RenderTileVector => RenderVector - Vectors.Origin;
        
        public Vector2 RenderOriginVector => RenderVector + Vectors.ToCenter;

        public Vector2 RenderVector => Vectors.Transform(Point);
    }
}
