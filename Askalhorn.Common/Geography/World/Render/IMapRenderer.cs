using Microsoft.Xna.Framework.Graphics;

namespace Askalhorn.Common.Geography.World.Render
{
    public interface IMapRenderer
    {
        void Draw(Map map, ref Texture2D texture);
    }
}