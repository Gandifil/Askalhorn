namespace Askalhorn.Common.Control
{
    public interface IMove
    {
        bool IsValid(ICharacter character);
        internal void Make(Character character);
    }
}