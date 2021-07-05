using System;
using Askalhorn.Common.Render;

namespace Askalhorn.Common.Geography.Local
{
    public interface IBuild
    {
        public enum Types { None, Chest, Teleport}
        
        IPosition Position { get; }
        
        Types Type { get; }

        IRenderer Renderer { get; }
        
        Action Action { get; } 
    }
}