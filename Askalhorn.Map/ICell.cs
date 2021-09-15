
using Askalhorn.Map.Builds;

namespace Askalhorn.Map
{
    public interface ICell
    {
        bool IsWall { get; }
        
        IBuild Build { get; }

        IGameObject DynamicObject { get; }
    }
}