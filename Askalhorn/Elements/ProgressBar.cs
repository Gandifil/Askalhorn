using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Askalhorn.Elements
{
    public class ProgressBar
    {
        public Color HighColor { get; set; } = Color.LightGreen;
        
        public Color LowColor { get; set; } = Color.DarkRed;
        
        public Color BorderColor { get; set; } = Color.Black;
        
        public Color BackgroundColor { get; set; } = Color.Gray;

        public int BorderWidth { get; set; } = 2;

        public int SplitWidth { get; set; } = 1;

        public int Width { get; set; } = 64;
        
        public int Height { get; set; } = 4;

        private Texture2D texture;
        
        public ProgressBar(SpriteBatch batch)
        {
            texture = new Texture2D(batch.GraphicsDevice, 1, 1);
            texture.SetData(new[] { Color.White });
        }
        
        public void Draw(SpriteBatch batch, Point position, float filling)
        {
            batch.Draw(
                texture,
                new Rectangle(
                    position + new Point(-BorderWidth, -BorderWidth), 
                    new Point(Width + 2* BorderWidth, Height + 2* BorderWidth)
                ), 
                BorderColor);
            
            
            batch.Draw(texture,
                new Rectangle(position, new Point( Width, Height)), 
                BackgroundColor);
            
            batch.Draw(texture,
                new Rectangle(position, new Point((int)(filling * Width), Height)), 
                Color.Lerp(LowColor, HighColor, filling));
            
            // draw grid
            int n = 5;
            for (int i = 1; i < n; i++)
            {
                int offset = Width * i / n ;
                batch.Draw(texture,
                    new Rectangle(
                        position + new Point(offset, 0), 
                        new Point(SplitWidth, Height)
                    ), 
                    BorderColor);
            }
        }
    }
}