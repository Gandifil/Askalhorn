using System.IO;
using Askalhorn.Common;
using Askalhorn.Math;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;
using Newtonsoft.Json;

namespace Askalhorn.Render
{
    public class TextureRenderer: IRenderer
    {
        [JsonIgnore]
        public readonly TextureRegion2D Region;

        public string FileName { get; set; }

        public Point? Position { get; set; }

        //public Point? Size { get; set; }
        
        [JsonConstructor]
        public TextureRenderer(string fileName, Point? position = null, Point? size = null)
        {
            FileName = fileName;
            Position = position;
            
            Texture2D texture = Storage.Content.Load<Texture2D>("images/" + fileName);
            
            if (position.HasValue)
            {
                var csize = size ?? new Point(64, 64);
                var location = position * csize;
                Region = new TextureRegion2D(texture, new Rectangle(location.Value, csize));
            }
            else
                Region = new TextureRegion2D(texture);

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
            batch.Draw(Region.Texture, center - Vectors.Origin, Region.Bounds, Color.White); //  - Vectors.Origin - Vectors.ToCenter
        }
    }
}