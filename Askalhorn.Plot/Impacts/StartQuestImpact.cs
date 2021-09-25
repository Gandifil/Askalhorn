using System;
using Askalhorn.Common;
using Askalhorn.Text;
using MonoGame.Extended.TextureAtlases;
using Newtonsoft.Json;
using Serilog;

namespace Askalhorn.Plot.Impacts
{
    public class StartQuestImpact: IImpact
    {
        public readonly string Quest;

        public readonly string Step;

        [JsonConstructor]
        [CommandConstructor]
        public StartQuestImpact(string quest, string step = null)
        {
            Quest = quest;
            Step = step;
        }
        
        public string Description { get; }
        public TextureRegion2D TextureRegion { get; }
        
        public void On(object target)
        {
            var objWithJournal = target as IHasJournal;
            if (objWithJournal is null)
                throw new ArgumentNullException(nameof(target), "Target must be a " + nameof(IHasJournal));
            
            IQuest quest = new Quest(Quest, Step);
            objWithJournal.Journal.Add(quest);
            
            Log.Information(new TextPointer("journal", "getQuestLog").ToString(), quest.Name);
        }
    }
}