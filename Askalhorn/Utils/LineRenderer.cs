using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace AmbrosiaGame.Utils
{
    public static class LineRenderer
    {
        private static Texture2D texture; 

        public static void Initialize(GraphicsDevice device)
        {
            texture = new Texture2D(device, 1, 1);
            texture.SetData(new Color[] { Color.White });
        }

        public static void Draw(SpriteBatch sb, Vector2 start, Vector2 end)
        {
            Vector2 edge = end - start;

            float angle = (float)Math.Atan2(edge.Y, edge.X);


            sb.Draw(texture,
                new Rectangle(// rectangle defines shape of line and position of start of line
                    (int)start.X,
                    (int)start.Y,
                    (int)edge.Length(), //sb will strech the texture to fill this rectangle
                    1), //width of line, change this to make thicker line
                null,
                Color.Red, //colour of line
                angle,     //angle of line (calulated above)
                new Vector2(0, 0), // point in line about which to rotate
                SpriteEffects.None,
                0);
        }
    }
}
