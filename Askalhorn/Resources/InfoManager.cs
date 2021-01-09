using Microsoft.Xna.Framework.Content;
using System;

namespace AmbrosiaGame.Resources
{
    public static class InfoManager
    {
        public static void Initialize(ContentManager content)
        {
            InfoManager.Content = content ?? throw new NullReferenceException();
        }

        public static ContentManager Content { get; private set; }
    }
}