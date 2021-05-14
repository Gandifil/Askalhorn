using Microsoft.Xna.Framework;
using MonoGame.Extended.Tiled;

namespace Askalhorn.Locations
{
    public interface ILocation
    {
        TiledMap Map { get; }

        void PlayerMoved(Vector2 point);


    }
}
