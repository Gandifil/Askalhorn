using System;
using Askalhorn.Common.Mechanics;
using Askalhorn.Common.Render;

namespace Askalhorn.Common.Geography.Local
{
    public interface IBuild: IGameObject
    {
        public enum Types { None, Chest, Teleport}
        
        Types Type { get; }
        
        IImpact Impact { get; }
    }
}