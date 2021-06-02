using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Common.Mechanics
{
    public interface IEffect
    {
        string Description { get; }
        
        uint TurnCount { get; }
        
        TextureRegion2D  TextureRegion { get; }
    }
}