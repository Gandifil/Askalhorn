using System.Collections.Generic;
using Askalhorn.Common.Characters;
using Askalhorn.Common.Control.Moves;
using MLEM.Pathfinding;

namespace Askalhorn.Common.Control
{
    internal class AgressiveController: IController
    {
        public IEnumerable<IMove> Moves => findMoves();
        
        public Character Parent { get; set; }

        public int Radius { get; set; } = 4;

        private Character target;

        private Character findTarget()
        {
            foreach (var character in World.Instance.Characters)
                if (Parent != character && character is Player &&
                    (Parent.Position.Point - character.Position.Point).ToVector2().Length() < Radius)
                    return (Character)character;
            return null;
        }

        private IEnumerable<IMove> findMoves()
        {
            if (target is null)
                target = findTarget();

            if (target is not null)
            {
                var pathfinder = new AStar2((pos, nextPos) => 1, 
                    false);
                var path = pathfinder.FindPath(Parent.Position.Point, target.Position.Point);
                path.Pop();
                return new List<IMove>{ new MovementToMove(path.Pop())};
            }

            return new List<IMove>();
        }
    }
}