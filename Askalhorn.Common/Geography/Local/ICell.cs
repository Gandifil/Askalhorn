using Askalhorn.Common.Characters;

namespace Askalhorn.Common.Geography.Local
{
    public interface ICell
    {
        bool IsWall { get; }

        ICharacter Character { get; }
    }
}