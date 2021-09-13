using Askalhorn.Common.Localization;

namespace Askalhorn.Common.Plot.Quests
{
    public class QuestStep: IQuestStep
    {
        public string Name { get; set; }

        public string NameOfLastStep { get; set; } = null;
        
        public TextPointer Description { get; set; }
    }
}