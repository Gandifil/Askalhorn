using System.Collections.Generic;

namespace Askalhorn.Common.Plot.Quests
{
    public interface IJournal: IEnumerable<IQuest>
    {
        //IReadOnlyCollection<IQuest> Quests { get; }
    }
}