using Askalhorn.Common;

namespace Askalhorn.Map.Builds
{
    public interface IBuild: IGameObject
    {
        public enum Types { None, Chest, Teleport}
        
        Types Type { get; }
    }
}