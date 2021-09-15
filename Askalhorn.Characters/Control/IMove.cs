namespace Askalhorn.Characters.Control
{
    public interface IMove
    {
        bool IsValid(ICharacter character);
        void Make(Character character);
    }
}