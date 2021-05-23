using System;
using System.Collections.Generic;
using System.Linq;
using Askalhorn.Common.Control.Moves;
using Microsoft.Xna.Framework;

namespace Askalhorn.Common.Control
{
    internal class RandomMovementController: IController
    {
        private readonly Random random = new Random();

        private readonly ICharacter character;

        public RandomMovementController(ICharacter character)
        {
            this.character = character;
        }

        public IEnumerable<IMove> Moves => new List<IMove>
        {
            getRandom(),
        };

        private IMove getRandom()
        {
            var list = character.CanMoveTo;
            return new MovementToMove(list.ElementAt(random.Next(0, list.Count())).Point);
        }
    }
}