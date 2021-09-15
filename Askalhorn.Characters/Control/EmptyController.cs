using System.Collections.Generic;

namespace Askalhorn.Characters.Control
{
    public class EmptyController: IController
    {
        public IEnumerable<IMove> Decide(ICharacter character)
        {
            return new List<IMove>();
        }
    }
}