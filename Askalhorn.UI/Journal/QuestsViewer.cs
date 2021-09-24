using System.Linq;
using Askalhorn.Plot;
using Askalhorn.UI.JournalW;
using MLEM.Ui;

namespace Askalhorn.UI.Journal
{
    public class QuestsViewer: InvisiblePanel
    {
        private readonly IJournal _journal;
        public readonly QuestListViewer List;
        
        public QuestsViewer(IJournal journal, Anchor anchor, float x, float y) : base(anchor, x, y)
        {
            _journal = journal;

            var filter = new QuestStateFilter(Anchor.AutoCenter, 1f, .1f);
            filter.OnPressFilter += SearchQuests;
            AddChild(filter);

            List = new QuestListViewer(Anchor.AutoCenter, 1f, .9f);
            List.SetupQuests(journal);
            AddChild(List);
        }

        private void SearchQuests(QuestState state)
        {
            List.SetupQuests(state == QuestState.None ? _journal : _journal.Where(x => x.State == state));
        }
    }
}