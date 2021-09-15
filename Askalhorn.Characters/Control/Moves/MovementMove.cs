using Askalhorn.Common;
using Askalhorn.Map;
using Askalhorn.Map.Local;
using Microsoft.Xna.Framework;
using Serilog;

namespace Askalhorn.Characters.Control.Moves
{
    /// <summary>
    /// Movement of character from one cell to another.
    /// </summary>
    public class MovementMove: IMove
    {
        /// <summary>
        /// The offset of movement
        /// </summary>
        public readonly Point Offset;
        
        public MovementMove(Point offset)
        {
            this.Offset = offset;
        }

        public bool IsValid(ICharacter character)
        {
            var location = Location.Current.Location;
            var pos = new Position(character.Position.Shift(Offset));
            return location.Contain(pos) && !location[pos].IsWall && location.Find(pos) is null;
        }

        void IMove.Make(Character character)
        {
            character.Position = (Position)character.Position.Shift(Offset);
            
            Log.Debug("Player are moving {Offset}", Offset);
        }
    }
}