using System.Collections.Generic;

namespace Askalhorn.Plot
{
    public interface IJournal
    {
        IReadOnlyCollection<IQuest> Quests { get; }
        IQuest Find(string name);
    }
}