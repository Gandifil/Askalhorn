using System.Collections.Generic;
using System.Linq;
using Askalhorn.Common.Characters;
using Askalhorn.Common.Control.Moves;
using Microsoft.Xna.Framework;
using MLEM.Pathfinding;

namespace Askalhorn.Common.Control
{
    internal class AgressiveController: IController
    {
        public int Radius { get; set; } = 4;

        private Character target;

        private Character findTarget(ICharacter currentCharacter)
        {
            foreach (var character in World.Instance.Characters)
                if (currentCharacter != character && character is Player &&
                    (currentCharacter.Position.Point - character.Position.Point).ToVector2().Length() < Radius)
                    return (Character)character;
            return null;
        }

        public IEnumerable<IMove> Decide(ICharacter character)
        {
            if (target is null)
                target = findTarget(character);

            if (target is not null)
            {
                if (new Rectangle(-1, -1, 3, 3).Contains(character.Position.Point - target.Position.Point))
                    return new List<IMove>
                    {
                        new UseAbilityMove()
                        {
                            Ability = character.Abilities.First(),
                            Target = target,
                        }
                    };
                else
                {
                    var pathfinder = new AStar2((pos, nextPos) => 1, 
                        false);
                    var path = pathfinder.FindPath(character.Position.Point, target.Position.Point);
                    path.Pop();
                    return new List<IMove>{ new MovementToMove(path.Pop())};
                }
            }

            return new List<IMove>();
        }
    }
}