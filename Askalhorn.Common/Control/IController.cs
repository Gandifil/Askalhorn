using System.Collections.Generic;

namespace Askalhorn.Common.Control
{
    public interface IController
    {
        IEnumerable<IMove> Decide(ICharacter character);
    }
}