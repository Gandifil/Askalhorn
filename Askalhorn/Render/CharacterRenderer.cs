using Askalhorn.Characters;
using Askalhorn.Common;
using Askalhorn.Elements;
using Askalhorn.Math;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Askalhorn.Render
{
    public class CharacterRenderer
    {
        private readonly ProgressBar hp;
        private readonly ProgressBar mp;
        private readonly SpriteFont font;

        public CharacterRenderer(SpriteBatch batch)
        {
            hp = new ProgressBar(batch);
            mp = new ProgressBar(batch)
            {
                HighColor = Color.BlueViolet,
                LowColor = Color.LightBlue,
            };
            font = Storage.Content.Load<SpriteFont>("fonts/GameLogsFont");
        }
        
        public void Draw(SpriteBatch batch, ICharacter character)
        {
            var target = character.Position.RenderVector;
            character.Renderer.Draw(batch, target);
            target -= Vectors.Origin;
            target.Y += 64;
            
            // hp
            hp.Draw(batch, target.ToPoint(), character.HP.Percent);
            
            // mp
            mp.Draw(batch, target.ToPoint() + new Point(0, 10), character.MP.Percent);
            
            // level
            var text = character.Level.Level.ToString();
            var textSize = font.MeasureString(text);
            target.X -= textSize.X + 5;
            
            batch.DrawString(font, text, target, Color.Chocolate);
        }
    }
}