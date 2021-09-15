using System;
using Askalhorn.Plot;
using MLEM.Ui;

namespace Askalhorn.Elements.Journal
{
    public class QuestListViewer: FixPanel
    {
        public QuestListViewer(IJournal journal, Anchor anchor, float width, float height) : base(anchor, width, height, true)
        {
            foreach (var quest in journal)
                AddChild(new QuestLine(quest, Anchor.AutoCenter, .95f, .1f)
                {
                    OnPressed = _ => OnOpened?.Invoke(quest),
                    
                });
        }

        public event Action<IQuest> OnOpened;
    }
}