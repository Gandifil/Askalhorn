using Microsoft.Xna.Framework;
using MonoGame.Extended.Tiled;

namespace Askalhorn.Common.Geography
{
    public interface ILocation
    {
        TiledMap TiledMap { get; }

        Local.ICell this[uint x, uint y] { get; }
    }
}
