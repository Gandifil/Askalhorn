using Askalhorn.Common.Geography.Local;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Common.Render
{
    public class TextureRenderer: IRenderer
    {
        public readonly TextureRegion2D Region;

        public TextureRenderer(string path, int x, int y, int width, int height)
        {
            Region = new TextureRegion2D(Storage.Content.Load<Texture2D>(path), x, y, width, height);
        }
        
        public void Dispose()
        {
            Region.Texture.Dispose();
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch batch, IPosition position)
        {
            batch.Draw(Region.Texture, position.RenderTileVector, Region.Bounds, Color.White);
        }
    }
}