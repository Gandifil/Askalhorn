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
        private Point offset;
        
        public MovementMove(Point offset)
        {
            this.offset = offset;
        }
        
        void IMove.Make(World world, Character character)
        {
            character.Position.Point = character.Position.Shift(offset);
            
            Log.Information("Player are moving {offset}", offset);
        }
    }
}