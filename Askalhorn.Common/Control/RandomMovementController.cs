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

        public IEnumerable<IMove> Decide(ICharacter character)
        {
            var list = character.AvailableMovements;
            return new List<IMove>
            {
                list.ElementAt(random.Next(0, list.Count())),
            };
        }
    }
}