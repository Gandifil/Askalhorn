﻿using Askalhorn.Common;
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
            hp1.Draw(batch, target.ToPoint(), character.HP.Percent);
            var font = Storage.Content.Load<SpriteFont>("fonts/GameLogsFont");
            batch.DrawString(font, character.Level.ToString(),target, Color.Chocolate);
        }
    }
}