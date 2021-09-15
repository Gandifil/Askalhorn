using System.Collections.Generic;

namespace Askalhorn.Characters.Control
{
    public interface IController
    {
        IEnumerable<IMove> Decide(ICharacter character);
    }
}