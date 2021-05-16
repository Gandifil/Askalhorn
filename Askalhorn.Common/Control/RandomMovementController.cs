using System;
using System.Collections.Generic;
using System.Linq;
using Askalhorn.Common.Control.Moves;
using Microsoft.Xna.Framework;

namespace Askalhorn.Common.Control
{
    internal class RandomMovementController: IController
    {
        private static readonly List<IMove> Variants = new List<IMove>
        {
            new MovementMove(new Point(0, -1)),
            new MovementMove(new Point(0, 1)),
            new MovementMove(new Point(1, 0)),
            new MovementMove(new Point(-1, 0)),
        };
        
        private readonly Random random = new Random();

        public IEnumerable<IMove> Moves => new List<IMove>
        {
            Variants[random.Next() % Variants.Count()],
        };
    }
}