using Askalhorn.Map.Local;

namespace Askalhorn.Map
{
    public interface IGameObjectBuilder
    {
        IGameObject Build(Position position);
    }
}