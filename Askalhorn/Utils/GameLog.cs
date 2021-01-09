using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbrosiaGame.Utils
{
    public static class GameLog
    {
        private static List<string> lines = new List<string>();
        private static SpriteFont font;
        private static Color color = new Color(0, 255, 0);

        private const int SHIFT = 16;
        private const int LINE_LIMIT = 10;

        public static void Write(string line)
        {
            lines.Add(line);
            if (lines.Count > LINE_LIMIT)
                lines.RemoveAt(0);
        }

        public static void Initialize(ContentManager content)
        {
            font = content.Load<SpriteFont>("fonts/GameLogsFont");
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            var position = new Vector2(0, spriteBatch.GraphicsDevice.Viewport.Height / 2);
            foreach (var line in lines)
            {
                spriteBatch.DrawString(font, line, position, color);
                position.Y += SHIFT;
            }
        }
    }
}
