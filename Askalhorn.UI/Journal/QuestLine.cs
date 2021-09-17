using Askalhorn.Plot;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.UI.Journal
{
    public class QuestLine: FixPanel
    {
        public QuestLine(IQuest quest, Anchor anchor, float width, float height) : base(anchor, width, height)
        {
            AddChild(new Paragraph(Anchor.Center, 1, quest.Name, true));
        }
    }
}