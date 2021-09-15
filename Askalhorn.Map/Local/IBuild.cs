using Askalhorn.Common;

namespace Askalhorn.Map.Local
{
    public interface IBuild: IGameObject
    {
        public enum Types { None, Chest, Teleport}
        
        Types Type { get; }
        
        IImpact Impact { get; }
    }
}