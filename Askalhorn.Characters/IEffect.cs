using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Characters
{
    public interface IEffect
    {
        string Description { get; }
        
        uint TurnCount { get; }
        
        TextureRegion2D  TextureRegion { get; }
    }
}