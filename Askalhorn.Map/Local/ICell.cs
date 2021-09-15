
namespace Askalhorn.Map.Local
{
    public interface ICell
    {
        bool IsWall { get; }
        
        IBuild Build { get; }

        //ICharacter Character { get; }
    }
}