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
        public readonly string QuestName;

        public readonly string QuestStep;

        [JsonConstructor]
        [CommandConstructor]
        public StartQuestImpact(string questName, string questStep = null)
        {
            QuestName = questName;
            QuestStep = questStep;
        }
        
        public string Description { get; }
        public TextureRegion2D TextureRegion { get; }
        
        public void On(object target)
        {
            var objWithJournal = target as IHasJournal;
            if (objWithJournal is null)
                throw new ArgumentNullException(nameof(target), "Target must be a " + nameof(IHasJournal));
            
            IQuest quest = new Quest(QuestName, QuestStep);
            objWithJournal.Journal.Add(quest);
            
            Log.Information(new TextPointer("journal", "getQuestLog").ToString(), quest.Name);
        }
    }
}