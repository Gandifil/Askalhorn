using Microsoft.Xna.Framework;

namespace Askalhorn.Common.Geography.Local
{
    public interface IPosition
    {
        Point Point { get; }
        Vector2 RenderTileVector { get; }
        Vector2 RenderOriginVector { get; }
        Vector2 RenderVector { get; }

        int X => Point.X;

        int Y => Point.Y;
    }
}