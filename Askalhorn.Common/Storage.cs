using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Askalhorn.Common
{
    public static class Storage
    {
        public static void Initialize(ContentManager content, GraphicsDevice graphicsDevice)
        {
            Content = content ?? throw new NullReferenceException();
            GraphicsDevice = graphicsDevice ?? throw new NullReferenceException();
        }

        public static GraphicsDevice GraphicsDevice { get; private set; }
        public static ContentManager Content { get; private set; }
    }
}