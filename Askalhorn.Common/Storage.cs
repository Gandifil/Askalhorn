using System;
using Microsoft.Xna.Framework.Content;

namespace Askalhorn.Common
{
    public static class Storage
    {
        public static void Initialize(ContentManager content)
        {
            Content = content ?? throw new NullReferenceException();
        }

        public static ContentManager Content { get; private set; }
    }
}