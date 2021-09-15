using Askalhorn.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;
using Newtonsoft.Json;

namespace Askalhorn.Render
{
    public class TextureRenderer: IRenderer
    {
        public readonly TextureRegion2D Region;
        
        public TextureRenderer(string fileName, int x, int y, int width, int height)
        {
            Region = new TextureRegion2D(Storage.Content.Load<Texture2D>(fileName), x, y, width, height);
        }
        
        public TextureRenderer(string fileName, uint x, uint y)
        {
            Region = Storage.Load(fileName, x, y);
        }
        
        [JsonConstructor]
        public TextureRenderer(string fileName, Point? position = null)
        {
            if (position.HasValue)
                Region = Storage.Load(fileName, (uint)position.Value.X, (uint)position.Value.Y);
            else
                Region = new TextureRegion2D(Storage.Content.Load<Texture2D>("images/" + fileName));
        }
        
        public void Dispose()
        {
            Region.Texture.Dispose();
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch batch, Vector2 center)
        {
            batch.Draw(Region.Texture, center, Region.Bounds, Color.White);
        }
    }
}