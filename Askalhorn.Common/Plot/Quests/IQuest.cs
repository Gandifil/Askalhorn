using System.Collections.Generic;

namespace Askalhorn.Common.Plot.Quests
{
    public interface IQuest
    {
        QuestState State { get; }
        string Name { get; }
        string Description { get; }
        //IReadOnlyCollection<IQuestStep> Steps { get; }
    }
}