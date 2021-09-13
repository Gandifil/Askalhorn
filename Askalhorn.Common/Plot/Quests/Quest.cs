using System.Collections.Generic;
using Askalhorn.Common.Localization;
using Newtonsoft.Json;

namespace Askalhorn.Common.Plot.Quests
{
    public class QuestFileReader : PolymorphJsonReader<QuestFile>
    {
        
    }
    public class QuestFile
    {
        public TextPointer Name { get; set; }
        
        public TextPointer Description { get; set; }
        
        public List<QuestStep> Steps { get; set; }
    }
    
    public sealed class Quest: IQuest
    {
        string IQuest.Name => _quest.Name.ToString();
        
        string IQuest.Description => _quest.Description.ToString();

        //IReadOnlyCollection<IQuestStep> IQuest.Steps => _quest.Steps;

        public QuestState State { get; }

        public string CurrentStep { get; set; }

        public readonly string ContentName;
        private readonly QuestFile _quest;

        [JsonConstructor]
        public Quest(string contentName, string step, QuestState state)
        {
            ContentName = contentName;
            State = state;
            CurrentStep = step;
            
            var filePath = $"quests/{ContentName}";
            _quest = Storage.Content.Load<QuestFile>(filePath);
        }

        public Quest(string contentName, string step = null)
        {
            ContentName = contentName;
            State = QuestState.InProgress;
            CurrentStep = step ?? null;
            
            var filePath = $"quests/{ContentName}";
            _quest = Storage.Content.Load<QuestFile>(filePath);
            
        }
    }
}