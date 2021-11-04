using System;
using MonoGame.Extended.TextureAtlases;
using Newtonsoft.Json;

namespace Askalhorn.Render
{
    public interface IIcon
    {
        TextureRenderer Renderer { get; }
        
        string TooltipText { get; }

        event Action OnChanged;
    }
}