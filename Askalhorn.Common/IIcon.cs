using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Common
{
    public interface IIcon
    {
        TextureRegion2D Texture { get; }
        
        string TooltipText { get; }
    }
}