using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace Askalhorn.Render
{
    public interface IRenderer: IDisposable, IUpdate
    {
        void Draw(SpriteBatch batch, Vector2 center);
    }
}