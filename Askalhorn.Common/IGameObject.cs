using Askalhorn.Common.Geography.Local;
using Askalhorn.Common.Render;

namespace Askalhorn.Common
{
    public interface IGameObject
    {
        Position Position { get; set; }
        IRenderer Renderer { get; set; }
    }
}