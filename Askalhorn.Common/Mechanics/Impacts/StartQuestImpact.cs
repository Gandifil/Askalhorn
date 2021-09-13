using System;
using Askalhorn.Common.Characters;
using Askalhorn.Common.Localization;
using Askalhorn.Common.Plot.Quests;
using MonoGame.Extended.TextureAtlases;
using Newtonsoft.Json;
using Serilog;

namespace Askalhorn.Common.Mechanics.Impacts
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
        public void On(Character character)
        {
            if (character is null)
                throw new ArgumentNullException(nameof(character));

            var player = character as Player;
            if (player is null)
                throw new ArgumentException(nameof(character), "Character must be a player!");

            IQuest quest = new Quest(QuestName, QuestStep);
            
            Log.Information(new TextPointer("journal", 3).ToString(), quest.Name);

            player.Journal.Quests.Add(quest);
        }
    }
}