using AmbrosiaGame.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace AmbrosiaGame.GameWorld
{
    class Creature: DrawableObject
    {

        public Creature(int x, int y, Texture2D texture)
            :base(x, y, texture)
        {
           
        }

        //public Body Body { get; protected set; }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            Vector2 pos = GetWorldPosition;
            
            //LineRenderer.Draw(spriteBatch, pos, pos + new Vector2(64 * Body.HP.Proportion, 0));
        }

        
    }
}
