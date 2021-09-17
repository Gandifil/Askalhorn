using Askalhorn.Map.Actions;

namespace Askalhorn.Characters.Control.Moves
{
    public class UseActionMove: IMove
    {
        public readonly IAction Action;

        public UseActionMove(IAction action)
        {
            Action = action;
        }
        
        public bool IsValid(ICharacter character)
        {
            return true;
        }

        public void Make(Character character)
        {
            Action.Impact.On(character);
        }
    }
}