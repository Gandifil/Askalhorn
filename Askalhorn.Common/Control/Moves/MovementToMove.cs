﻿using Askalhorn.Common.Geography.Local;
using Microsoft.Xna.Framework;
using Serilog;

namespace Askalhorn.Common.Control.Moves
{
    /// <summary>
    /// Movement of character from one cell to another.
    /// </summary>
    public class MovementToMove: IMove
    {
        /// <summary>
        /// The target of movement
        /// </summary>
        private Point target;
        
        public MovementToMove(Point target)
        {
            this.target = target;
        }

        public bool IsValid(ICharacter character)
        {
            var location = World.Instance.Location;
            var pos = new Position(target);
            return location.Contain(pos) && !World.Instance.Location[pos].IsWall;
        }

        void IMove.Make(Character character)
        {
            character.Position.Point = target;
            
            Log.Information("Player are moving to {target}", target);
        }
    }
}