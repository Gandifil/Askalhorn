using Microsoft.Xna.Framework.Graphics;

namespace Askalhorn.Locations.World
{
    public interface IMapRenderer
    {
        void Draw(Map map, ref Texture2D texture);
    }
}