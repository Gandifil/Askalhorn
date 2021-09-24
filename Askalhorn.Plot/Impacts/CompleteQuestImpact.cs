using System;
using Askalhorn.Common;
using MonoGame.Extended.TextureAtlases;
using Newtonsoft.Json;

namespace Askalhorn.Plot.Impacts
{
    public class CompleteQuestImpact: IImpact
    {
        public string Description { get; }
        public TextureRegion2D TextureRegion { get; }

        public readonly string Quest;

        public readonly string Step;

        [JsonConstructor]
        [CommandConstructor]
        public CompleteQuestImpact(string quest, string step = null)
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
            quest.Complete(Step);
        }
    }
}