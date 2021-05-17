using Microsoft.Xna.Framework;

namespace Askalhorn.Common.Geography.Local
{
    public interface IPosition
    {
        Point Point { get; }
        Vector2 RenderVectorOrigin { get; }
        Vector2 RenderVector { get; }

        int X => Point.X;

        int Y => Point.Y;
    }
}