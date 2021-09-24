using System;
using Askalhorn.Common;
using MonoGame.Extended.TextureAtlases;
using Newtonsoft.Json;

namespace Askalhorn.Plot.Impacts
{
    public class MoveQuestImpact: IImpact
    {
        public string Description { get; }
        public TextureRegion2D TextureRegion { get; }

        public string Quest { get; }

        public string Step { get; }

        [JsonConstructor]
        [CommandConstructor]
        public MoveQuestImpact(string quest, string step)
        {
            Quest = quest;
            Step = step;
        }
        
        public void On(object target)
        {
            var objWithJournal = target as IHasJournal;
            if (objWithJournal is null)
                throw new ArgumentNullException(nameof(target), "Target must be a " + nameof(IHasJournal));

            var quest = objWithJournal.Journal.Find(Quest);
            quest.CurrentStep = Step;
        }
    }
}