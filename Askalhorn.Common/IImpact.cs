using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Common
{
    public interface IImpact
    {
        string Description { get; }
        
        TextureRegion2D  TextureRegion { get; }
        
        void On(object target);
    }
}