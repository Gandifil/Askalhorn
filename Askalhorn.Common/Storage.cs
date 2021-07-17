using System;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Common
{
    public static class Storage
    {
        public static void Initialize(ContentManager content, GraphicsDevice graphicsDevice)
        {
            Content = content ?? throw new NullReferenceException();
            GraphicsDevice = graphicsDevice ?? throw new NullReferenceException();
        }

        public static TextureRegion2D Load(string filename, uint x, uint y)
        {
            return new TextureRegion2D(
                Content.Load<Texture2D>("images/" + filename),
                (int)x * 64, (int)y * 64,
                64, 64
            );
        }

        public static SoundEffect LoadSound(string filename)
        {
            return Content.Load<SoundEffect>("sounds/"+filename);
        }

        public static GraphicsDevice GraphicsDevice { get; private set; }
        public static ContentManager Content { get; private set; }

        public static Random Random = new Random();
    }
}