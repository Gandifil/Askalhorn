using System;
using Askalhorn.Common.Interpetators;

namespace Askalhorn.Plot
{
    public class QuestRequirement: BoolExpression
    {
        public string Quest { get; }
        
        public string Step { get; }

        public QuestRequirement(string quest, string step = null)
        {
            Quest = quest;
            Step = step;
        }
        
        protected override bool PrivateCalculate(object target, Random random)
        {
            var objWithJournal = target as IHasReadOnlyJournal;
            if (objWithJournal is null)
                throw new ArgumentNullException(nameof(target), "Target must be a " + nameof(IHasReadOnlyJournal));

            var quest = objWithJournal.Journal.Find(Quest);
            if (quest is null)
                return false;
            
            return string.IsNullOrEmpty(Step) ? true : (quest as Quest).CurrentStep == Step;
        }
    }
}