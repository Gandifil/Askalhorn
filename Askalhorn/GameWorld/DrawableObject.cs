using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AmbrosiaGame.GameWorld
{
    class DrawableObject: PositionObject
    {
        private Texture2D texture;

        public DrawableObject(int x, int y, Texture2D info)
            : base(x, y)
        {
            texture = info ?? throw new ArgumentException();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, GetWorldPosition - new Vector2(32, 48), Color.Transparent);
        }
    }
}
