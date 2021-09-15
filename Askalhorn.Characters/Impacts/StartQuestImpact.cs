using System;
using Askalhorn.Common;
using Askalhorn.Plot;
using Askalhorn.Text;
using MonoGame.Extended.TextureAtlases;
using Newtonsoft.Json;
using Serilog;

namespace Askalhorn.Characters.Impacts
{
    public class StartQuestImpact: IImpact
    {
        public readonly string QuestName;

        public readonly string QuestStep;

        [JsonConstructor]
        public StartQuestImpact(string questName, string questStep = null)
        {
            QuestName = questName;
            QuestStep = questStep;
        }
        
        public string Description { get; }
        public TextureRegion2D TextureRegion { get; }
        public void On(object target)
        {
            var character = target as Character;
            if (character is null)
                throw new ArgumentNullException(nameof(target));

            var player = character as Player;
            if (player is null)
                throw new ArgumentException(nameof(character), "Character must be a player!");

            IQuest quest = new Quest(QuestName, QuestStep);
            
            Log.Information(new TextPointer("journal", "getQuestLog").ToString(), quest.Name);

            player.Journal.Quests.Add(quest);
        }
    }
}