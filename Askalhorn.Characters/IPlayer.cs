using Askalhorn.Characters.Control;
using Askalhorn.Plot;

namespace Askalhorn.Characters
{
    public interface IPlayer: ICharacter
    {
        IJournal Journal { get; }

        public void Make(IMove move);
    }
}