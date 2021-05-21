using System;
using Askalhorn.Common.Render;

namespace Askalhorn.Common.Geography.Local
{
    public interface IBuild
    {
        IPosition Position { get; }

        IRenderer Renderer { get; }
        
        Action Action { get; } 
    }
}