using Askalhorn.Common.Geography.Local;
using Microsoft.Xna.Framework;
using Serilog;

namespace Askalhorn.Common.Control.Moves
{
    /// <summary>
    /// Movement of character from one cell to another.
    /// </summary>
    public class MovementMove: IMove
    {
        /// <summary>
        /// The offset of movement
        /// </summary>
        public Point Offset { get; set; }
        
        public MovementMove(Point offset)
        {
            this.Offset = offset;
        }

        public bool IsValid(ICharacter character)
        {
            var location = World.Instance.Location;
            var pos = new Position(character.Position.Shift(Offset));
            return location.Contain(pos) && !World.Instance.Location[pos].IsWall && World.Instance.Find(pos) is null;
        }

        void IMove.Make(Character character)
        {
            character.Position.Point = character.Position.Shift(Offset);
            
            Log.Debug("Player are moving {Offset}", Offset);
        }
    }
}