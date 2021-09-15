using System.Collections.Generic;
using System.Linq;
using Askalhorn.Characters.Control.Moves;
using Askalhorn.Common;
using Askalhorn.Map;
using Microsoft.Xna.Framework;
using MLEM.Pathfinding;

namespace Askalhorn.Characters.Control
{
    internal class AgressiveController: IController
    {
        public int Radius { get; set; } = 4;

        private Character target;

        private Character findTarget(ICharacter currentCharacter)
        {
            foreach (var gameObjects in Location.Current.Location.GameObjects)
                if (currentCharacter != gameObjects && gameObjects is Player &&
                    (currentCharacter.Position.Point - gameObjects.Position.Point).ToVector2().Length() < Radius)
                    return (Character)gameObjects;
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