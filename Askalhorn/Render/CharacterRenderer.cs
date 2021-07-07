using Askalhorn.Common;
using Askalhorn.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Askalhorn.Render
{
    public class CharacterRenderer
    {
        public void Draw(SpriteBatch batch, ICharacter character)
        {
            var target = character.Position.RenderTileVector;
            batch.Draw(character.Texture, target, Color.White);
            target.Y += 64;
            
            // hp
            var hp = new ProgressBar(batch);
            hp.Draw(batch, target.ToPoint(), character.HP.Percent);
            
            // mp
            var mp = new ProgressBar(batch)
            {
                HighColor = Color.BlueViolet,
                LowColor = Color.LightBlue,
            };
            mp.Draw(batch, target.ToPoint() + new Point(0, 10), character.MP.Percent);
            
            // level
            var font = Storage.Content.Load<SpriteFont>("fonts/GameLogsFont");
            batch.DrawString(font, character.Level.ToString(),target + new Vector2(-10, 0), Color.Chocolate);
        }
    }
}