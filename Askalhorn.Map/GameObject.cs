using Askalhorn.Map.Local;
using Askalhorn.Render;

namespace Askalhorn.Map
{
    public class GameObject : IGameObject
    {
        public Position Position { get; set; }
        
        public IRenderer Renderer { get; set; } 
    }
}