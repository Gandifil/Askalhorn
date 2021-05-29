

using Microsoft.Xna.Framework;

namespace Askalhorn.Common.Control.Moves
{
    public class AttackMove: IMove
    {
        public ICharacter Target { get; set; }

        public AttackMove(ICharacter character)
        {
            this.Target = character;
        }
        
        public bool IsValid(ICharacter character)
        {
            var rect = new Rectangle(character.Position.Point - new Point(1, 1), new Point(3, 3));
            return rect.Contains(Target.Position.Point);
        }

        void IMove.Make(Character character)
        {
            var p = (Character) Target;
        }
    }
}