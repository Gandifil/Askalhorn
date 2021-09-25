using System;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Render
{
    public interface IIcon
    {
        TextureRenderer Renderer { get; }
        
        string TooltipText { get; }

        event Action OnChanged;
    }
}