﻿using System.Collections.Generic;
using System.Linq;

namespace Askalhorn.Common.Control
{
    public class BufferController: IController
    {
        private List<IMove> buffer = new List<IMove>();

        public void AddMove(IMove move)
        {
            var world = World.Instance;
            if (move.IsValid(world.Player))
            {
                buffer.Add(move);
                world.Turn();
            }
        }

        public IEnumerable<IMove> Decide(ICharacter character)
        {
            var results = buffer.ToList();
            buffer.Clear();
            return results;
        }
    }
}