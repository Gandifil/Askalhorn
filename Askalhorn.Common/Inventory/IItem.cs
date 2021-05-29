using Askalhorn.Common.Render;
using MonoGame.Extended;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Common.Inventory
{
    public interface IItem
    {
        string Name { get; }
        
        string Description { get; }
        
        TextureRegion2D Texture { get; }
        
        float Weight { get; }
        
        Size Size { get; }
    }
}