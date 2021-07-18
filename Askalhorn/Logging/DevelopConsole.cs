using System.Collections.Generic;
using System.Reflection.Metadata;
using Askalhorn.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Askalhorn.Logging
{
    public class DevelopConsole
    {
        private readonly SpriteFont font;
        
        private const int SHIFT = 16;

        public Color Color { get; set; } = Color.Red;
        public Vector2 Position { get; set; } = new Vector2(0, 200);
        

        public DevelopConsole(SpriteFont font)
        {
            this.font = font;
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
            var position = Position;
            
            batch.Begin();
            foreach (var line in StringLineStorage.Logs)
            {
                batch.DrawString(font, line, position, Color);
                position.Y += SHIFT;
            }
            batch.End();
        }
    }
}
