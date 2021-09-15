using System.Collections.Generic;
using System.Linq;
using Askalhorn.Common;

namespace Askalhorn.Characters.Control
{
    public class BufferController: IController
    {
        private List<IMove> buffer = new List<IMove>();

        public void AddMove(IMove move, ICharacter character)
        {
            if (move is not null && move.IsValid(character))
            {
                buffer.Add(move);
                TurnRunner.Instance.Turn();
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