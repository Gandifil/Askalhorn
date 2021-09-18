using System;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Render
{
    public interface IIcon
    {
        TextureRegion2D Texture { get; }
        
        string TooltipText { get; }

        event Action OnChanged;
    }
}