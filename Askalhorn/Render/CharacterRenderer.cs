using Askalhorn.Common;
using Askalhorn.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Askalhorn.Render
{
    public class CharacterRenderer
    {
        private readonly ProgressBar hp;
            
        public void Draw(SpriteBatch batch, ICharacter character)
        {
            var target = character.Position.RenderTileVector;
            batch.Draw(character.Texture, target, Color.White);
            ProgressBar hp1 = new ProgressBar(batch);
            target.Y += 64;
            hp1.Draw(batch, target.ToPoint(), (float)character.HP.Value / character.MaxHP.Value);
        }
    }
}