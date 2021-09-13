using Askalhorn.Common.Maths;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace Askalhorn.Common.Geography.Local
{
    /// <summary>
    /// IPosition implementation via <see cref="T:Microsoft.Xna.Framework.Point" />
    /// </summary>
    public class Position: IPosition
    {
        [JsonIgnore]
        public Point Point { get; set; }

        public uint X => (uint)Point.X;

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
        [JsonConstructor]
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

        public bool IsInside(IPosition center, float radius)
        {
            return (center.Point - Point).ToVector2().Length() < radius;
        }
        
        [JsonIgnore]
        public Vector2 RenderTileVector => RenderVector - Vectors.Origin;
        
        [JsonIgnore]
        public Vector2 RenderOriginVector => RenderVector + Vectors.ToCenter;

        [JsonIgnore]
        public Vector2 RenderVector => Vectors.Transform(Point);
        
        /// <summary>
        /// Returns a <see cref="T:System.String" /> representation of this <see cref="T:Askalhorn.Common.Geography.Local.Position" /> in the format:
        /// (X: <see cref="P:Askalhorn.Common.Geography.Local.Position.X" />, Y:<see cref="P:Askalhorn.Common.Geography.Local.Position.Y" />)
        /// </summary>
        /// <returns><see cref="T:System.String" /> representation of this <see cref="T:Askalhorn.Common.Geography.Local.Position" />.</returns>
        public override string ToString() => $"({X}, {Y})";
    }
}
