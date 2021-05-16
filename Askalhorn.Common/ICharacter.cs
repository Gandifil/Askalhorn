using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Askalhorn.Common
{
    public interface ICharacter
    {
        public string Name { get; }

        void Draw(SpriteBatch batch, Matrix matrix);

    }
}