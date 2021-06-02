using Microsoft.Xna.Framework;

namespace Askalhorn.Common.Geography.Local
{
    /// <summary>
    /// Describe a position of a object on tile map in 2d-space. Can return vectors for rendering object.
    /// </summary>
    public interface IPosition
    {
        /// <summary>
        /// The point of tile map.
        /// </summary>
        Point Point { get; }
        
        /// <summary>
        /// The vector of top left angle of object tile.
        /// </summary>
        Vector2 RenderTileVector { get; }
        
        /// <summary>
        /// The vector of tile's center.
        /// </summary>
        Vector2 RenderOriginVector { get; }
        
        /// <summary>
        /// The vector of tile's top.
        /// </summary>
        Vector2 RenderVector { get; }

        /// <summary>
        /// The x coordinate.
        /// </summary>
        uint X { get; }


        /// <summary>
        /// The y coordinate.
        /// </summary>
        uint Y { get; }

        /// <summary>
        /// Return a new position equals current + direction.
        /// </summary>
        /// <param name="direction">The direction of shifting</param>
        /// <returns>Shifted position</returns>
        IPosition Shift(Point direction);
    }
}