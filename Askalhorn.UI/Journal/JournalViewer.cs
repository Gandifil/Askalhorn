using System.Linq;
using Askalhorn.Plot;
using Askalhorn.Text;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.UI.Journal
{
    public class JournalViewer: FixPanel
    {
        public JournalViewer(IJournal journal, Anchor anchor, float width, float height) : base(anchor, width, height)
        {
            AddChild(new Paragraph(Anchor.TopCenter, 1, new TextPointer("journal", "title").ToString(), true));
            
            var list = new QuestsViewer(journal, Anchor.BottomLeft, .49f, .95f);
            list.List.OnOpened += OpenQuest;
            AddChild(list);
            
            if (journal.Any())
                OpenQuest(journal.First());
        }

        private QuestViewer _quest = null;
        
        private void OpenQuest(IQuest quest)
        {
            if (_quest is not null)
                _quest.Dispose();

            _quest = new QuestViewer(quest, Anchor.BottomRight, .49f, .95f);
            AddChild(_quest);
        }
    }
}