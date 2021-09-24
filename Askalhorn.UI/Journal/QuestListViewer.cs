using System;
using System.Collections;
using System.Collections.Generic;
using Askalhorn.Plot;
using MLEM.Ui;

namespace Askalhorn.UI.Journal
{
    public class QuestListViewer: FixPanel
    {
        public QuestListViewer(Anchor anchor, float width, float height) : base(anchor, width, height, true)
        {
        }

        public void SetupQuests(IEnumerable<IQuest> quests)
        {
            RemoveChildren();
            
            foreach (var quest in quests)
                AddChild(new QuestLine(quest, Anchor.AutoCenter, .95f, .1f)
                {
                    OnPressed = _ => OnOpened?.Invoke(quest),
                });
        }

        public event Action<IQuest> OnOpened;
    }
}