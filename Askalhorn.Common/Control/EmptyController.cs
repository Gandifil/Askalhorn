using System.Collections.Generic;

namespace Askalhorn.Common.Control
{
    public class EmptyController: IController
    {
        public IEnumerable<IMove> Moves => new List<IMove>();
    }
}