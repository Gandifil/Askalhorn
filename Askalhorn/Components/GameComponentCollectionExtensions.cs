using System;
using Microsoft.Xna.Framework;

namespace Askalhorn.Components
{
    public static class GameComponentCollectionExtensions
    {
        public static void Dispose(this GameComponentCollection components, int index)
        {
            (components[index] as IDisposable)?.Dispose();
            components.RemoveAt(index);
        }

        public static void ClearWithDispose(this GameComponentCollection components)
        {
            for (int i = components.Count - 1; i > 0; i--)
                components.Dispose(i);
        }
    }
}