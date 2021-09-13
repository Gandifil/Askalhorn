using Askalhorn.Common.Geography.Local;
using Askalhorn.Common.Render;

namespace Askalhorn.Common
{
    public class GameObject : IGameObject
    {
        public Position Position { get; set; }
        
        public IRenderer Renderer { get; set; } 
    }
}