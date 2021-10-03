using System.Collections.Generic;
using Askalhorn.Characters.Control;
using Askalhorn.Plot;

namespace Askalhorn.Characters
{
    public interface IPlayer: ICharacter, IHasReadOnlyJournal
    {
        void Make(IMove move);
        
        HotKeys HotKeys { get; }
    }
}