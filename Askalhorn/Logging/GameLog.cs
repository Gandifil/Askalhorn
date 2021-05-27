using System.Collections.Generic;
using Askalhorn.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Askalhorn.Logging
{
    public class GameLog
    {
        private readonly SpriteFont font;

        private readonly Color color;
        private const int SHIFT = 16;

        public GameLog(SpriteFont font, Color color)
        {
            this.font = font;
            this.color = color;
        }
        
        /// <summary>
        /// Writes log events to <see cref="Askalhorn.Logging.StringLineStorage"/>.
        /// </summary>
        /// <param name="sinkConfiguration">Logger sink configuration.</param>
        /// <param name="outputTemplate">A message template describing the format used to write to the sink.
        /// The default is <code>"[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"</code>.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        /// <exception cref="ArgumentNullException">When <paramref name="sinkConfiguration"/> is <code>null</code></exception>
        public void Draw(SpriteBatch batch)
        {
            var position = new Vector2(0, batch.GraphicsDevice.Viewport.Height / 2);
            batch.Begin();
            foreach (var line in StringLineStorage.Logs)
            {
                batch.DrawString(font, line, position, color);
                position.Y += SHIFT;
            }
            batch.End();
        }
    }
}
