using System.Collections.Generic;

namespace Askalhorn.Common.Control
{
    public interface IController
    {
        IEnumerable<IMove> Moves { get; }
    }
}