using System;
using Askalhorn.Common;
using MonoGame.Extended.TextureAtlases;
using Newtonsoft.Json;

namespace Askalhorn.Plot.Impacts
{
    public class FailQuestImpact: IImpact
    {
        public string Description { get; }
        public TextureRegion2D TextureRegion { get; }

        public string Quest { get; }

        [JsonConstructor]
        [CommandConstructor]
        public FailQuestImpact(string quest)
        {
            Quest = quest;
        }
        
        public void On(object target)
        {
            var objWithJournal = target as IHasJournal;
            if (objWithJournal is null)
                throw new ArgumentNullException(nameof(target), "Target must be a " + nameof(IHasJournal));

            var quest = objWithJournal.Journal.Find(Quest);
            quest.Fail();
        }
    }
}