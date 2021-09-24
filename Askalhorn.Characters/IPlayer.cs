using Askalhorn.Characters.Control;
using Askalhorn.Plot;

namespace Askalhorn.Characters
{
    public interface IPlayer: ICharacter, IHasReadOnlyJournal
    {
        public void Make(IMove move);
    }
}