using Askalhorn.Common.Geography.Local;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Askalhorn.Common.Characters
{
    public interface ICharacter
    {
        void Draw(SpriteBatch batch, Matrix matrix);
        
        public Position Position { get; }
    }
}