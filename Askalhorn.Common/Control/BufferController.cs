using System.Collections.Generic;
using System.Linq;

namespace Askalhorn.Common.Control
{
    public class BufferController: IController
    {
        public IEnumerable<IMove> Moves
        {
            get
            {
                var results = buffer.ToList();
                buffer.Clear();
                return results;
            }
        }

        private List<IMove> buffer = new List<IMove>();

        public void AddMove(IMove move)
        {
            buffer.Add(move);
            World.Instance.Turn();
        }
    }
}