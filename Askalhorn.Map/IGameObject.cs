using Askalhorn.Map.Local;
using Askalhorn.Render;

namespace Askalhorn.Map
{
    public interface IGameObject
    {
        Position Position { get; set; }
        IRenderer Renderer { get; set; }
    }
}