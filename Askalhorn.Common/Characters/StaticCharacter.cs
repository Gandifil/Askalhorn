using Askalhorn.Common.Control;
using Askalhorn.Common.Geography.Local;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Askalhorn.Common.Characters
{
    public class StaticCharacter: ICharacter
    {
        public IController Controller { get; set; }
        public Texture2D Texture { get; set; }

        public Position Position { get; set; }

        private static readonly Vector2 Origin = new Vector2(32, 32);

        public void Draw(SpriteBatch batch, Matrix matrix)
        {
            batch.Draw(Texture, Position.RenderVector - Origin, Color.White);
        }
    }
}