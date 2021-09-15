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
    public class MovementToMove: IMove
    {
        /// <summary>
        /// The target of movement
        /// </summary>
        public readonly Point target;
        
        public MovementToMove(Point target)
        {
            this.target = target;
        }

        public bool IsValid(ICharacter character)
        {
            var location = Location.Current.Location;
            var pos = new Position(target);
            return location.Contain(pos) && !location[pos].IsWall && location[pos].DynamicObject is null;
        }

        void IMove.Make(Character character)
        {
            character.Position = new Position(target);
            
            Log.Debug("Player are moving to {target}", target);
        }
    }
}