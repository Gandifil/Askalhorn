using Askalhorn.Common.Characters;
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
        /// Offset of movement
        /// </summary>
        private Point offset;
        
        public MovementMove(Point offset)
        {
            this.offset = offset;
        }
        
        public void Make(World world, ICharacter character)
        {
            character.Position.Move(offset);
            
            Log.Verbose("Shift player with offset {offset} to {Point}", offset, character.Position.Point);
        }
    }
}