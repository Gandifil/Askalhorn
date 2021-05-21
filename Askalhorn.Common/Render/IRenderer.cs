using System;
using Askalhorn.Common.Geography.Local;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace Askalhorn.Common.Render
{
    public interface IRenderer: IDisposable, IUpdate
    {
        void Draw(SpriteBatch batch, IPosition position);
    }
}