using Askalhorn.Render;

namespace Askalhorn.Characters.Effects
{
    public interface IEffect: IIcon
    {
        uint TurnCount { get; }
    }
}