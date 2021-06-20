using System.Collections.Generic;

namespace Askalhorn.Common.Plot.Quests
{
    public interface IQuest
    {
        string Name { get; }
        string Description { get; }
        IReadOnlyCollection<IQuestStep> Steps { get; }
    }
}