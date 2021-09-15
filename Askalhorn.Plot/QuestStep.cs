using Askalhorn.Text;

namespace Askalhorn.Plot
{
    public class QuestStep: IQuestStep
    {
        public string Name { get; set; }

        public string NameOfLastStep { get; set; } = null;
        
        public TextPointer Description { get; set; }
    }
}