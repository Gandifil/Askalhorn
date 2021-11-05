using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Askalhorn.Render
{
    public class EmptyRenderer: IRenderer
    {
        public void Dispose()
        {
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch batch, Vector2 center)
        {
        }
    }
}