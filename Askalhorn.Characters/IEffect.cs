using Askalhorn.Render;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Characters
{
    public interface IEffect: IIcon
    {
        uint TurnCount { get; }
    }
}