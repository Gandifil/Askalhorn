using Askalhorn.Common.Characters;

namespace Askalhorn.Common.Control
{
    public interface IMove
    {
        void Make(World world, ICharacter character);
    }
}