using System.Collections.Generic;

namespace Askalhorn.Common.Plot.Quests
{
    public class Quest: IQuest
    {
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public IReadOnlyCollection<IQuestStep> Steps { get; }
    }
}